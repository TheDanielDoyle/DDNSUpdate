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
        private static readonly string _createDNSRecordFormat = "v1/{0}/records";
        private static readonly string _createDNSRecordFailureMessageTemplate = "Unable to create DNS records for domian {0}";
        private static readonly string _createDNSRecordSuccessMessageTemplate = "Successfully created GoDaddyClient DNS records for domian {0}";
        private static readonly string _getDNSRecordsFormat = "domains/{0}/records/{1}";
        private static readonly string _getDNSRecordsFailureMessageTemplate = "Unable to retrieve DNS records for {0}";
        private static readonly string _getDNSRecordsSuccessMessageTemplate = "Successfully retrieved DNS records for {0}";
        private static readonly string _ssoKey = "sso-key";
        private static readonly string _updateDNSRecordsFormat = "domains/{0}/records/{1}";
        private static readonly string _updateDNSRecordsFailureMessageTemplate = "Unable to update DNS records for {0}";
        private static readonly string _updateDNSRecordsSuccessMessageTemplate = "Successfully updated GoDaddy Domain DNS records for Doamin {0}";

        private readonly IFlurlClient _httpClient;

        public GoDaddyClient(HttpClient httpClient)
        {
            _httpClient = new FlurlClient(httpClient);
        }

        public async Task<Result> CreateDNSRecordsAsync(GoDaddyCreateDNSRecordRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_createDNSRecordFormat, request.DomainName);
            IFlurlRequest httpRequest = BuildRequest(request.ApiKey, request.ApiSecret, path);
            HttpResponseMessage response = await httpRequest.PatchJsonAsync(request, cancellation);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                IEnumerable<GoDaddyGetDNSRecordResponse> records
                    = JsonConvert.DeserializeObject<List<GoDaddyGetDNSRecordResponse>>(content);
                string resultMessage = string.Format(_createDNSRecordSuccessMessageTemplate, request.DomainName);
                return Result.Ok(new GoDaddyGetDNSRecordsResponse(records)).WithSuccess(resultMessage);
            }
            return Result.Fail(string.Format(_createDNSRecordFailureMessageTemplate, request.DomainName));
        }

        public async Task<Result<GoDaddyGetDNSRecordsResponse>> GetDNSRecordsAsync(GoDaddyGetDNSRecordsRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_getDNSRecordsFormat, request.DomainName, request.DNSRecordType.Value);
            IFlurlRequest httpRequest = BuildRequest(request.ApiKey, request.ApiSecret, path);
            HttpResponseMessage response = await httpRequest.GetAsync(cancellation);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                IEnumerable<GoDaddyGetDNSRecordResponse> records
                    = JsonConvert.DeserializeObject<List<GoDaddyGetDNSRecordResponse>>(content);
                string resultMessage = string.Format(_getDNSRecordsSuccessMessageTemplate, request.DomainName);
                return Result.Ok(new GoDaddyGetDNSRecordsResponse(records)).WithSuccess(resultMessage);
            }
            return Result.Fail(string.Format(_getDNSRecordsFailureMessageTemplate, request.DomainName));
        }

        public async Task<Result> UpdateDNSRecordsAsync(GoDaddyUpdateDNSRecordsRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_updateDNSRecordsFormat, request.DomainName, request.DNSRecordType.Value);
            string json = JsonConvert.SerializeObject(request.Records);
            IFlurlRequest httpRequest = BuildRequest(request.ApiKey, request.ApiSecret, path);
            HttpResponseMessage response = await httpRequest.PutJsonAsync(json, cancellation);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string resultMessage = string.Format(_updateDNSRecordsSuccessMessageTemplate, request.DomainName);
                return Result.Ok().WithSuccess(resultMessage);
            }
            return Result.Fail(string.Format(_updateDNSRecordsFailureMessageTemplate, request.DomainName));
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
