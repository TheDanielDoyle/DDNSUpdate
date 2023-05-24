using System.Collections.Generic;

namespace DDNSUpdate.Application;

internal sealed record UpdateFailed(IEnumerable<string> ErrorMessages)
{
    public UpdateFailed(string errorMessage) : this(new[] { errorMessage } )
    {
    }
}