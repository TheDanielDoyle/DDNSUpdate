using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using FluentResults;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanClient : IDigitalOceanClient
    {
        private static readonly Uri _apiBase = new Uri("https://api.digitalocean.com/v2/");
        private static readonly string _createDNSRecordFormat = "domains/{0}/records";
        private static readonly string _getDNSRecordFormat = "domains/{0}/records";
        private static readonly string _updateDNSRecordFormat = "domains/{0}/records/{1}";

        private readonly IFlurlClient _httpClient;

        public DigitalOceanClient(HttpClient httpClient)
        {
            _httpClient = new FlurlClient(httpClient);
        }

        public async Task<Result> CreateDNSRecordAsync(string domainName, DigitalOceanCreateDomainRecordRequest request, string token, CancellationToken cancellation)
        {
            string path = string.Format(_createDNSRecordFormat, domainName);
            IFlurlRequest httpRequest = BuildRequest(token, path);
            HttpResponseMessage response = await httpRequest.PostJsonAsync(request, cancellation);
            bool created = (int)response.StatusCode == (int)HttpStatusCode.Created;
            return Result.OkIf(created, $"Unable to create DNS record for {request.Name}.");
        }

        public async Task<Result<DigitalOceanGetDomainRecordsResponse>> GetDNSRecordsAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation)
        {
            string path = string.Format(_getDNSRecordFormat, domain.Name);
            IFlurlRequest httpRequest = BuildRequest(token, path);
            HttpResponseMessage response = await httpRequest.GetAsync(cancellation);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return Result.Ok(JsonConvert.DeserializeObject<DigitalOceanGetDomainRecordsResponse>(content));
            }
            return Result.Fail($"Unable to retrieve DNS records for {domain.Name}");
        }

        public async Task<Result> UpdateDNSRecordAsync(string domainName, DigitalOceanUpdateDomainRecordRequest request, string token, CancellationToken cancellation)
        {
            string path = string.Format(_updateDNSRecordFormat, domainName, request.Id);
            IFlurlRequest httpRequest = BuildRequest(token, path);
            HttpResponseMessage response = await httpRequest.PutJsonAsync(request, cancellation);
            bool updated = (int) response.StatusCode == (int) HttpStatusCode.OK;
            return Result.OkIf(updated, $"Unable to update DNS record for {request.Name}.");
        }

        private IFlurlRequest BuildRequest(string token, string path)
        {
            return _httpClient
                .AllowAnyHttpStatus()
                .Request(_apiBase)
                .AppendPathSegment(path)
                .WithOAuthBearerToken(token);
        }
    }
}
