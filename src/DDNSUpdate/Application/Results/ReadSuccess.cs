using System.Collections.ObjectModel;

namespace DDNSUpdate.Application.Results;

internal sealed record ReadSuccess<TRecord>(ReadOnlyCollection<TRecord> Records, string Message);