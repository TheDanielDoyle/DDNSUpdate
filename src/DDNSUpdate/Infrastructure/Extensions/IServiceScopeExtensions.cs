using Microsoft.Extensions.DependencyInjection;

namespace DDNSUpdate.Infrastructure.Extensions;

internal static class IServiceScopeExtensions
{
    public static T GetRequiredService<T>(this IServiceScope scope) where T : notnull
    {
        return scope.ServiceProvider.GetRequiredService<T>();
    }
}