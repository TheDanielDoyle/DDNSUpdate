using OneOf;

namespace DDNSUpdate.Application.Results;

[GenerateOneOf]
internal partial class WriteRecordsResult : OneOfBase<WriteSuccess, WriteFailed, WriteCancelled>
{
}