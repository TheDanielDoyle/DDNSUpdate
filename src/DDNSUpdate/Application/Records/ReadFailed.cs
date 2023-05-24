using System.Collections.Generic;

namespace DDNSUpdate.Application.Records;

internal sealed record ReadFailed(IEnumerable<string> ErrorMessages)
{
    public ReadFailed(string errorMessage) : this(new[] { errorMessage } )
    {
    }
}