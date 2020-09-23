using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDNSRecordCreater : IDigitalOceanDNSRecordCreater
    {
        private readonly IDigitalOceanClient _digitalOceanClient;

        public DigitalOceanDNSRecordCreater(IDigitalOceanClient digitalOceanClient)
        {
            _digitalOceanClient = digitalOceanClient;
        }

        public async Task<Result> CreateAsync(DNSRecordCollection dnsRecords, string token, CancellationToken cancellation)
        {
            Result result = Result.Ok();
            foreach (DNSRecord dnsRecord in dnsRecords)
            {
                Result createResult = await _digitalOceanClient.CreateDNSRecordAsync(dnsRecord, token, cancellation);
                if (createResult.IsFailed)
                {
                    Result.Merge(result, createResult);
                }
            }
            return result;
        }
    }
}