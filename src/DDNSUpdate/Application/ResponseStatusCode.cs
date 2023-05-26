namespace DDNSUpdate.Application;

internal static class ResponseStatusCode
{
    public const int Ok200 = 200;
    public const int Unauthorized401 = 401;
    public const int ResourceNotFound404 = 404;
    public const int RateLimitExceeded429 = 429;
    public const int ServerError500 = 500;
}