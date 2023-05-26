using OneOf;

namespace DDNSUpdate.Application.Results;

[GenerateOneOf]
internal partial class ReadRecordsResult<TRecord> : OneOfBase<ReadSuccess<TRecord>, ReadFailed, ReadCancelled>
{
}