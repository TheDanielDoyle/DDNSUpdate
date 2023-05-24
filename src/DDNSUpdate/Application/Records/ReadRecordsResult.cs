using OneOf;

namespace DDNSUpdate.Application.Records;

[GenerateOneOf]
internal partial class ReadRecordsResult<TRecord> : OneOfBase<ReadSuccess<TRecord>, ReadFailed, ReadCancelled>
{
}