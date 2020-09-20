namespace DDNSUpdate.Domain
{
    public class DNSRecord
    {
        public string Data { get; set; } = default!;

        public int? Flags { get; set; }

        public string Name { get; set; } = default!;

        public int? Port { get; set; }

        public int? Priority { get; set; }

        public string? Tag { get; set; }

        public int TTL { get; set; }

        public DNSRecordType Type { get; set; } = default!;

        public int? Weight { get; set; }
    }
}
