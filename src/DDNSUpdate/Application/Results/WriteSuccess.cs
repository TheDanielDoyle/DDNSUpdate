namespace DDNSUpdate.Application.Results;

internal sealed record WriteSuccess(int RecordsCreated, int RecordsUpdated, string Message);