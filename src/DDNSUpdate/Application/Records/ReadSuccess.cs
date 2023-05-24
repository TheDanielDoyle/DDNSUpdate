using System.Collections.ObjectModel;

namespace DDNSUpdate.Application.Records;

internal sealed record ReadSuccess<TRecord>(ReadOnlyCollection<TRecord> Records, string Message);