using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyDNSRecordCreator : IGoDaddyDNSRecordCreator
    {
        private readonly IGoDaddyClient _client;

        public GoDaddyDNSRecordCreator(IGoDaddyClient client)
        {
            _client = client;
        }

        public async Task<Result> CreateAsync(string domainName, DNSRecordCollection records, string apiKey, string apiSecret, CancellationToken cancellation)
        {
            GoDaddyCreateDNSRecordRequest? request = new GoDaddyCreateDNSRecordRequest(apiKey, apiSecret, records, domainName);
            return await _client.CreateDNSRecordAsync(request, cancellation);
        }
    }
}