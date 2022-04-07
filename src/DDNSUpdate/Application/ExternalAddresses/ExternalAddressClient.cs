using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Configuration;
using FluentResults;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public class ExternalAddressClient : IExternalAddressClient
    {
        public static readonly string _errorMessage = "Cannot process DNS records - Unable to get external IP address.";
        public static readonly string _successMessageTemplate = "Successfully found external IP address {0}";

        private readonly IOptionsSnapshot<ApplicationConfiguration> _configuration;
        private readonly IFlurlClient _httpClient;

        public ExternalAddressClient(IOptionsSnapshot<ApplicationConfiguration> configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = new FlurlClient(httpClient);
        }

        public async Task<Result<IExternalAddressResponse>> GetAsync(CancellationToken cancellation)
        {
            foreach (ExternalAddressProvider provider in _configuration.Value.ExternalAddressProviders)
            {
                Result<IPAddress> result = await GetExternalIPAddressAsync(provider, cancellation);
                if (result.IsSuccess)
                {
                    ExternalAddress externalAddress = new ExternalAddress { IPv4Address = result.Value };
                    return Result.Ok<IExternalAddressResponse>(new ExternalAddressResponse(externalAddress));
                }
            }
            return Result.Fail<IExternalAddressResponse>(_errorMessage);
        }

        private async Task<Result<IPAddress>> GetExternalIPAddressAsync(ExternalAddressProvider provider, CancellationToken cancellation)
        {
            string? response = await _httpClient
                .AllowAnyHttpStatus()
                .Request(provider.Uri)
                .GetStringAsync(cancellation);

            if (IPAddress.TryParse(response, out IPAddress? ipAddress))
            {
                return Result.Ok(ipAddress).WithSuccess(string.Format(_successMessageTemplate, ipAddress));
            }
            return Result.Fail(_errorMessage);
        }
    }
}
