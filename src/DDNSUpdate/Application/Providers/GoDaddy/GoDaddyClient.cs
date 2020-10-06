using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using FluentResults;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyClient : IGoDaddyClient
    {
        private static readonly Uri _apiBase = new Uri("https://api.godaddy.com");
        private static readonly string _authorizationHeader = "Authorization";
        private static readonly string _getDNSRecordsFormat = "domains/{0}/records/{1}";
        private static readonly string _ssoKey = "sso-key";
        private static readonly string _updateDNSRecordsFormat = "domains/{0}/records/{1}";

        private readonly IFlurlClient _httpClient;

        public GoDaddyClient(HttpClient httpClient)
        {
            _httpClient = new FlurlClient(httpClient);
        }

        public async Task<Result<GoDaddyGetDNSRecordsResponse>> GetDNSRecordsAsync(GoDaddyGetDNSRecordsRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_getDNSRecordsFormat, request.DomainName, request.DNSRecordType.Value);
            HttpResponseMessage response = await BuildRequest(request.ApiKey, request.ApiSecret, path).GetAsync(cancellation);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                IEnumerable<GoDaddyGetDNSRecordResponse> records
                    = JsonConvert.DeserializeObject<List<GoDaddyGetDNSRecordResponse>>(content);
                return Result.Ok(new GoDaddyGetDNSRecordsResponse(records));
            }
            return Result.Fail($"Unable to retrieve DNS records for {request.DomainName}");
        }

        public async Task<Result> ReplaceDNSRecordsAsync(GoDaddyUpdateDNSRecordsRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_updateDNSRecordsFormat, request.DomainName, request.DNSRecordType.Value);
            string json = JsonConvert.SerializeObject(request.Records);
            HttpResponseMessage response = await BuildRequest(request.ApiKey, request.ApiSecret, path).PutJsonAsync(json, cancellation);
            return Result.OkIf(response.StatusCode == HttpStatusCode.OK, $"Unable to update DNS for {request.DomainName}");
        }

        private IFlurlRequest BuildRequest(string apiKey, string apiSecret, string path)
        {
            return _httpClient
                .AllowAnyHttpStatus()
                .Request(_apiBase)
                .WithHeader(_authorizationHeader, GenerateAuthorizationHeader(apiKey, apiSecret))
                .AppendPathSegment(path);
        }

        private string GenerateAuthorizationHeader(string apiKey, string apiSecret)
        {
            return $"{_ssoKey} {apiKey}:{apiSecret}";
        }
    }
}
