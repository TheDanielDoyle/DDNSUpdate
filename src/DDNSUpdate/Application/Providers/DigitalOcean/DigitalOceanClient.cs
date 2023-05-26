using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;
using DDNSUpdate.Infrastructure;
using Flurl.Http;
using static DDNSUpdate.Application.ResponseStatusCode;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed class DigitalOceanClient : IDigitalOceanClient
{
    private readonly IFlurlClient _client;
    private readonly DigitalOceanSettings _settings;

    public DigitalOceanClient(HttpClient client, DigitalOceanSettings settings)
    {
        _client = new FlurlClient(client).Configure(s =>
        {
            s.JsonSerializer = new SystemJsonSerializer();
        });
        _settings = settings;
    }

    public async Task<DigitalOceanReadRecordsResult> GetRecordsAsync(string domain, string bearerToken, CancellationToken cancellation)
    {
        // TODO: Handle paging
        IFlurlResponse response = await BuildRequest(bearerToken)
            .AppendPathSegments("domain", domain, "records")
            .SetQueryParam("per_page", 200.ToString())
            .GetAsync(cancellation);

        return response.StatusCode switch
        {
            //TODO: Tweak each error to have appropriate message
            Ok200 => new Ok<DigitalOceanReadDomainRecordsResponse>(await response.GetJsonAsync<DigitalOceanReadDomainRecordsResponse>()),
            Unauthorized401 => new Unauthorized(), 
            ResourceNotFound404 => new ResourceNotFound(),
            RateLimitExceeded429 => new RateLimitExceeded(),
            ServerError500 => new ServerError(),
            _ => new ServerError()
        };
    }
    
    private IFlurlRequest BuildRequest(string bearerToken)
    {
        return _client
            .AllowAnyHttpStatus()
            .Request(_settings.ApiUrl)
            .WithOAuthBearerToken(bearerToken);
    }
}