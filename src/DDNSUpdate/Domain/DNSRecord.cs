namespace DDNSUpdate.Domain
{
    public record DNSRecord
    {
        public string Data { get; init; } = default!;

        public int? Flags { get; init; }

        public string? Id { get; init; }

        public string Name { get; init; } = default!;

        public int? Port { get; init; }

        public int? Priority { get; init; }

        public string? Tag { get; init; }

        public int? TTL { get; init; }

        public DNSRecordType Type { get; init; } = default!;

        public int? Weight { get; init; }
    }
}
