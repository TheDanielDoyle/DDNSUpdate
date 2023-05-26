using OneOf;

namespace DDNSUpdate.Application.Results;

[GenerateOneOf]
internal partial class UpdateResult : OneOfBase<UpdateSuccess, UpdateFailed, UpdateCancelled>
{
}