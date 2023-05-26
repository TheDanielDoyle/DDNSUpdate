namespace DDNSUpdate.Application.Results;

internal sealed record UpdateSuccess(int RecordsCreated, int RecordsUpdated, string Message);