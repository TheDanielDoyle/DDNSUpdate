
namespace DDNSUpdate.Application.Providers.GoDaddy.Domain
{
    public class GoDaddyAuthenticationDetails
    {
        public GoDaddyAuthenticationDetails(string apiKey, string apiSecret)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }

        public string ApiKey { get; }

        public string ApiSecret { get; }
    }
}
