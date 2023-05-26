namespace DDNSUpdate.Application.Providers.DigitalOcean;

internal sealed record DigitalOceanDomainRecord
{
    public string? Data { get; set; }
    
    public int? Flags { get; init; }
    
    public string? Id { get; init; }

    public int? Port { get; init; }

    public int? Priority { get; init; }
    
    public string? Tag { get; init; }
    
    public string? Type { get; set; }
    
    public int? Weight { get; init; }
}