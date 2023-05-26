namespace DDNSUpdate.Application.Providers.GoDaddy;

internal sealed record GoDaddyAccount
{
    public string? ApiKey { get; set; }
    
    private string? ApiSecret { get; set; }
}