using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Application.Providers.GoDaddy.Response;
using FluentResults;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyClient : IGoDaddyClient
    {
        private static readonly Uri _apiBase = new Uri("https://api.godaddy.com/");
        private static readonly string _authorizationHeader = "Authorization";
        private static readonly string _createDNSRecordFormat = "v1/domains/{0}/records";
        private static readonly string _createDNSRecordFailureMessageTemplate = "Unable to create DNS records for domain {0}";
        private static readonly string _createDNSRecordSuccessMessageTemplate = "Successfully created {0} GoDaddy DNS records for domain {1}";
        private static readonly string _getDNSRecordsFormat = "v1/domains/{0}/records/{1}";
        private static readonly string _getDNSRecordsFailureMessageTemplate = "Unable to retrieve DNS records for {0}";
        private static readonly string _getDNSRecordsSuccessMessageTemplate = "{0} DNS records retrieved for GoDaddy domain {1}";
        private static readonly string _ssoKey = "sso-key";
        private static readonly string _updateDNSRecordsFormat = "v1/domains/{0}/records/{1}/{2}";
        private static readonly string _updateDNSRecordsFailureMessageTemplate = "Unable to update DNS record {0} for GoDaddy domain {1}";
        private static readonly string _updateDNSRecordsSuccessMessageTemplate = "Successfully updated GoDaddy domain {0} DNS record {1}";

        private readonly IFlurlClient _httpClient;

        public GoDaddyClient(HttpClient httpClient)
        {
            _httpClient = new FlurlClient(httpClient);
        }

        public async Task<Result> CreateDNSRecordsAsync(GoDaddyCreateDNSRecordsRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_createDNSRecordFormat, request.DomainName);
            IFlurlRequest httpRequest = BuildRequest(request.ApiKey, request.ApiSecret, path);
            IFlurlResponse response = await httpRequest.PatchJsonAsync(request.Records, cancellation);
            HttpResponseMessage message = response.ResponseMessage;
            if (message.StatusCode == HttpStatusCode.OK)
            {
                return Result.Ok().WithSuccess(string.Format(_createDNSRecordSuccessMessageTemplate, request.Records.Count(), request.DomainName));
            }
            return Result.Fail(string.Format(_createDNSRecordFailureMessageTemplate, request.DomainName));
        }

        public async Task<Result<GoDaddyGetDNSRecordsResponse>> GetDNSRecordsAsync(GoDaddyGetDNSRecordsRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_getDNSRecordsFormat, request.DomainName, request.DNSRecordType.Value);
            IFlurlRequest httpRequest = BuildRequest(request.ApiKey, request.ApiSecret, path);
            IFlurlResponse response = await httpRequest.GetAsync(cancellation);
            HttpResponseMessage message = response.ResponseMessage;
            if (message.StatusCode == HttpStatusCode.OK)
            {
                string content = await message.Content.ReadAsStringAsync();
                IEnumerable<GoDaddyGetDNSRecordResponse> records = JsonConvert.DeserializeObject<List<GoDaddyGetDNSRecordResponse>>(content);
                string resultMessage = string.Format(_getDNSRecordsSuccessMessageTemplate, records.Count(),  request.DomainName);
                return Result.Ok(new GoDaddyGetDNSRecordsResponse(records)).WithSuccess(resultMessage);
            }
            return Result.Fail(string.Format(_getDNSRecordsFailureMessageTemplate, request.DomainName));
        }

        public async Task<Result> UpdateDNSRecordAsync(GoDaddyUpdateDNSRecordsRequest request, CancellationToken cancellation)
        {
            string path = string.Format(_updateDNSRecordsFormat, request.DomainName, request.RecordType.Value, request.RecordName);
            IFlurlRequest httpRequest = BuildRequest(request.ApiKey, request.ApiSecret, path);
            IFlurlResponse response = await httpRequest.PutJsonAsync(new [] { request.Record }, cancellation);
            HttpResponseMessage message = response.ResponseMessage;
            if (message.StatusCode == HttpStatusCode.OK)
            {
                string resultMessage = string.Format(_updateDNSRecordsSuccessMessageTemplate, request.DomainName, request.RecordName);
                return Result.Ok().WithSuccess(resultMessage);
            }
            return Result.Fail(string.Format(_updateDNSRecordsFailureMessageTemplate, request.RecordName, request.DomainName));
        }

        private IFlurlRequest BuildRequest(string apiKey, string apiSecret, string path)
        {
            return _httpClient
                .AllowAnyHttpStatus()
                .Request(_apiBase)
                .WithHeader(_authorizationHeader, GenerateAuthorizationHeader(apiKey, apiSecret))
                .AppendPathSegment(path);
        }

        private static string GenerateAuthorizationHeader(string apiKey, string apiSecret)
        {
            return $"{_ssoKey} {apiKey}:{apiSecret}";
        }
    }
}
