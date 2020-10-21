using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FluentResults;

public interface IGoDaddyDNSRecordUpdater
{
    Task<Result> UpdateAsync(string domainName, DNSRecordCollection records, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation);
}