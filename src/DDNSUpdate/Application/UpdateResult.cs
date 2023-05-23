using OneOf;

namespace DDNSUpdate.Application;

[GenerateOneOf]
internal partial class UpdateResult : OneOfBase<UpdateSuccess, UpdateFailed, UpdateCancelled>
{
}