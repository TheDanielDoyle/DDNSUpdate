using System.Collections.Generic;

namespace DDNSUpdate.Application.Results;

internal sealed record WriteFailed(IEnumerable<string> ErrorMessages)
{
    public WriteFailed(string errorMessage) : this(new[] { errorMessage } )
    {
    }
}