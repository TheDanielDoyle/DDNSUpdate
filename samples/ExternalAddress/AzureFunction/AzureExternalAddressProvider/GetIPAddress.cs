using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace AzureExternalAddressProvider
{
    public static class GetIPAddress
    {
        [FunctionName("GetIpAddress")]
        public static async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            return req.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
