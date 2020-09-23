using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDNSRecordUpdater : IDigitalOceanDNSRecordUpdater
    {
        private readonly IDigitalOceanClient _digitalOceanClient;

        public DigitalOceanDNSRecordUpdater(IDigitalOceanClient digitalOceanClient)
        {
            _digitalOceanClient = digitalOceanClient;
        }

        public async Task<Result> UpdateAsync(DNSRecordCollection dnsRecords, string token, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            foreach (DNSRecord dnsRecord in dnsRecords)
            {
                Result createResult = await _digitalOceanClient.UpdateDNSRecordAsync(dnsRecord, token, cancellation);
                if (result.IsFailed)
                {
                    Result.Merge(result, createResult);
                }
            }
            return result;
        }
    }
}