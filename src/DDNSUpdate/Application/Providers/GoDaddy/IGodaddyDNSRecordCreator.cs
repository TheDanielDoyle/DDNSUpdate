using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy;

public interface IGoDaddyDNSRecordCreator
{
    Task<Result> CreateAsync(string domainName, DNSRecordCollection records, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation);
}