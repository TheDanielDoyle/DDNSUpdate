using OneOf;

namespace DDNSUpdate.Application.Records;

[GenerateOneOf]
internal partial class WriteRecordsResult : OneOfBase<WriteSuccess, WriteFailed, WriteCancelled>
{
}