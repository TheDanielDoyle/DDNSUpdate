using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDomainProcessor : IDigitalOceanDomainProcessor
    {
        private readonly IDigitalOceanDNSRecordCreator _dnsRecordCreator;
        private readonly IDNSRecordCollectionExternalAddressHydrater _dnsRecordHydrater;
        private readonly IDigitalOceanDNSRecordReader _dnsRecordReader;
        private readonly IDigitalOceanDNSRecordUpdater _dnsRecordUpdater;

        public DigitalOceanDomainProcessor(IDNSRecordCollectionExternalAddressHydrater dnsRecordHydrater, IDigitalOceanDNSRecordCreator dnsRecordCreator, IDigitalOceanDNSRecordReader dnsRecordReader, IDigitalOceanDNSRecordUpdater dnsRecordUpdater)
        {
            _dnsRecordHydrater = dnsRecordHydrater;
            _dnsRecordCreator = dnsRecordCreator;
            _dnsRecordReader = dnsRecordReader;
            _dnsRecordUpdater = dnsRecordUpdater;
        }

        public async Task<Result> ProcessAsync(DigitalOceanDomain domain, ExternalAddress externalAddress, string token, CancellationToken cancellation)
        {
            Result<DNSRecordCollection> activeDnsRecordsResult = await _dnsRecordReader.ReadAsync(domain, token, cancellation);
            if (activeDnsRecordsResult.IsFailed)
            {
                return activeDnsRecordsResult;
            }

            DNSRecordCollection hydratedDnsRecords = _dnsRecordHydrater.Hydrate(domain.Records, externalAddress, DNSRecordType.A);

            Result create = await _dnsRecordCreator.CreateAsync(activeDnsRecordsResult.Value.WhereNew(hydratedDnsRecords), token, cancellation);
            Result update = await _dnsRecordUpdater.UpdateAsync(activeDnsRecordsResult.Value.WhereUpdated(hydratedDnsRecords), token, cancellation);
            return create.Merge(update);
        }
    }
}
