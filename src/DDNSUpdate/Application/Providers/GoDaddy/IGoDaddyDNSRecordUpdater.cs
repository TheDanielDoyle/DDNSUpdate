using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Domain;
using FluentResults;

public interface IGoDaddyDNSRecordUpdater
{
    Task<Result> UpdateAsync(string domainName, DNSRecordCollection records, string apiKey, string apiSecret, CancellationToken cancellation);
}