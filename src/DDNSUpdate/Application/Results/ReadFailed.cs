using System.Collections.Generic;

namespace DDNSUpdate.Application.Results;

internal sealed record ReadFailed(IEnumerable<string> ErrorMessages)
{
    public ReadFailed(string errorMessage) : this(new[] { errorMessage } )
    {
    }
}