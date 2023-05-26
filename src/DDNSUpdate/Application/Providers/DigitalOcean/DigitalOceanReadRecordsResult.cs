using DDNSUpdate.Application.Results;
using OneOf;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

[GenerateOneOf]
internal partial class DigitalOceanReadRecordsResult : OneOfBase<Ok<DigitalOceanReadDomainRecordsResponse>, Unauthorized, ResourceNotFound, RateLimitExceeded, ServerError>
{
}