namespace DDNSUpdate.Application.Records;

internal sealed record WriteSuccess(int RecordsCreated, int RecordsUpdated, string Message);