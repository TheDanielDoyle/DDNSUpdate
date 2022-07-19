using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FluentResults;

namespace DDNSUpdate.Application.Providers.GoDaddy;

public interface IGoDaddyDNSRecordReader
{
    Task<Result<DNSRecordCollection>> ReadAsync(string domainName, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation);
}