using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Application.Providers.DigitalOcean.Responses;
using FluentResults;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanClient : IDigitalOceanClient
    {
        private static readonly Uri _apiBase = new Uri("https://api.digitalocean.com/v2/");
        private static readonly string _createDNSRecordFormat = "domains/{0}/records";
        private static readonly string _createDNSRecordFailureMessageTemplate = "Unable to create DigitalOcean domain {0} DNS record for {1}";
        private static readonly string _createDNSRecordSuccessMessageTemplate = "Successfully created DigitalOcean domain {0} DNS record for {1}";
        private static readonly string _getDNSRecordsFormat = "domains/{0}/records";
        private static readonly string _getDNSRecordsFailureMessageTemplate = "Unable to retrieve DigitalOcean domain DNS records for {0}";
        private static readonly string _getDNSRecordsSuccessMessageTemplate = "{0} DNS records retrieved for DigitalOcean domain {1}";
        private static readonly string _updateDNSRecordFormat = "domains/{0}/records/{1}";
        private static readonly string _updateDNSRecordsFailureMessageTemplate = "Unable to update DNS record {0} for DigitalOcean domain {1}";
        private static readonly string _updateDNSRecordsSuccessMessageTemplate = "Successfully updated DigitalOcean domain {0} DNS record for {1}";

        private readonly IFlurlClient _httpClient;

        public DigitalOceanClient(HttpClient httpClient)
        {
            _httpClient = new FlurlClient(httpClient);
        }

        public async Task<Result> CreateDNSRecordAsync(string domainName, DigitalOceanCreateDomainRecordRequest request, string token, CancellationToken cancellation)
        {
            string path = string.Format(_createDNSRecordFormat, domainName);
            IFlurlRequest httpRequest = BuildRequest(token, path);
            IFlurlResponse response = await httpRequest.PostJsonAsync(request, cancellation);
            if (response.ResponseMessage.IsSuccessStatusCode)
            {
                return Result.Ok().WithSuccess(string.Format(_createDNSRecordSuccessMessageTemplate, domainName, request.Name));
            }
            return Result.Fail(string.Format(_createDNSRecordFailureMessageTemplate, domainName, request.Name));
        }

        public async Task<Result<DigitalOceanGetDomainRecordsResponse>> GetDNSRecordsAsync(DigitalOceanDomain domain, string token, CancellationToken cancellation)
        {
            string path = string.Format(_getDNSRecordsFormat, domain.Name);
            IFlurlRequest httpRequest = BuildRequest(token, path);
            IFlurlResponse response = await httpRequest.GetAsync(cancellation);
            HttpResponseMessage message = response.ResponseMessage;
            if (message.IsSuccessStatusCode)
            {
                string content = await message.Content.ReadAsStringAsync();
                DigitalOceanGetDomainRecordsResponse records = JsonConvert.DeserializeObject<DigitalOceanGetDomainRecordsResponse>(content);
                return Result.Ok(records).WithSuccess(string.Format(_getDNSRecordsSuccessMessageTemplate, records.DomainRecords.Count(), domain.Name));
            }
            return Result.Fail(string.Format(_getDNSRecordsFailureMessageTemplate, domain.Name));
        }

        public async Task<Result> UpdateDNSRecordAsync(string domainName, DigitalOceanUpdateDomainRecordRequest request, string token, CancellationToken cancellation)
        {
            string path = string.Format(_updateDNSRecordFormat, domainName, request.Id);
            IFlurlRequest httpRequest = BuildRequest(token, path);
            IFlurlResponse response = await httpRequest.PutJsonAsync(request, cancellation);
            HttpResponseMessage message = response.ResponseMessage;
            if (message.IsSuccessStatusCode)
            {
                return Result.Ok().WithSuccess(string.Format(_updateDNSRecordsSuccessMessageTemplate, domainName, request.Name));
            }
            return Result.Fail(string.Format(_updateDNSRecordsFailureMessageTemplate, request.Name, domainName));
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
