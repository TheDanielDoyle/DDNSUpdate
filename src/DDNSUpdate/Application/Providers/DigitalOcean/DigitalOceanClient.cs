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

        public async Task<Result> CreateDNSRecordAsync(DigitalOceanCreateDomainRecordRequest request, string token, CancellationToken cancellation)
        {
            string path = string.Format(_createDNSRecordFormat, request.Name);
            string json = JsonConvert.SerializeObject(request);
            HttpResponseMessage response = await BuildRequest(token, path).PostJsonAsync(json, cancellation);
            return Result.OkIf(response.StatusCode == HttpStatusCode.Created, $"Unable to create DNS record for {request.Name}.");
        }

        public async Task<Result<DigitalOceanGetDomainRecordsResponse>> GetDNSRecordsAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation)
        {
            string path = string.Format(_getDNSRecordFormat, domain.Name);
            HttpResponseMessage response = await BuildRequest(token, path).GetAsync(cancellation);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return Result.Ok(JsonConvert.DeserializeObject<DigitalOceanGetDomainRecordsResponse>(content));
            }
            return Result.Fail($"Unable to retrieve DNS records for {domain.Name}");
        }

        public async Task<Result> UpdateDNSRecordAsync(DigitalOceanUpdateDomainRecordRequest request, string token, CancellationToken cancellation)
        {
            string path = string.Format(_updateDNSRecordFormat, request.Name, request.Id);
            string json = JsonConvert.SerializeObject(request);
            HttpResponseMessage response = await BuildRequest(token, path).PutJsonAsync(json, cancellation);
            return Result.OkIf(response.StatusCode == HttpStatusCode.OK, $"Unable to update DNS record for {request.Name}.");
        }

        private IFlurlRequest BuildRequest(string token, string path)
        {
            return _httpClient
                .AllowAnyHttpStatus()
                .WithOAuthBearerToken(token)
                .Request(_apiBase)
                .AppendPathSegment(path);
        }
    }
}
