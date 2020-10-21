using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyDomainProcessor : IGoDaddyDomainProcessor
    {
        private readonly IGoDaddyDNSRecordCreator _dnsRecordCreator;
        private readonly IDNSRecordCollectionHydrater _dnsRecordHydrater;
        private readonly IGoDaddyDNSRecordReader _dnsRecordReader;
        private readonly IGoDaddyDNSRecordUpdater _dnsRecordUpdater;

        public GoDaddyDomainProcessor(
            IGoDaddyDNSRecordCreator dnsRecordCreator,
            IDNSRecordCollectionHydrater dnsRecordHydrater,
            IGoDaddyDNSRecordReader dnsRecordReader,
            IGoDaddyDNSRecordUpdater dnsRecordUpdater)
        {
            _dnsRecordCreator = dnsRecordCreator;
            _dnsRecordHydrater = dnsRecordHydrater;
            _dnsRecordReader = dnsRecordReader;
            _dnsRecordUpdater = dnsRecordUpdater;
        }

        public async Task<Result> ProcessAsync(GoDaddyDomain domain, ExternalAddress externalAddress, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation)
        {
            Result<DNSRecordCollection> activeDnsRecordsResult = await _dnsRecordReader.ReadAsync(domain.Name, authentication, cancellation);
            if (activeDnsRecordsResult.IsFailed)
            {
                return activeDnsRecordsResult;
            }

            DNSRecordCollection configurationRecords = new DNSRecordCollection(domain.Records);
            DNSRecordCollection hydratedDnsRecords = _dnsRecordHydrater.Hydrate(configurationRecords, activeDnsRecordsResult.Value, externalAddress, DNSRecordType.A);
            DNSRecordCollection newRecords = activeDnsRecordsResult.Value.WhereNew(hydratedDnsRecords);
            DNSRecordCollection updatedRecords = activeDnsRecordsResult.Value.WhereUpdated(hydratedDnsRecords);

            Result create = await _dnsRecordCreator.CreateAsync(domain.Name, newRecords, authentication, cancellation);
            Result update = await _dnsRecordUpdater.UpdateAsync(domain.Name, updatedRecords, authentication, cancellation);
            return activeDnsRecordsResult.Merge(create, update);
        }
    }
}
