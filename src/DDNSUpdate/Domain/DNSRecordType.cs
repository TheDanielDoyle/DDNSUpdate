using System.ComponentModel;
using Ardalis.SmartEnum;
using DDNSUpdate.Infrastructure.ComponentModel;

namespace DDNSUpdate.Domain
{
    [TypeDescriptionProvider(typeof(SmartEnumDescriptionProvider))]
    public sealed class DNSRecordType : SmartEnum<DNSRecordType, string>
    {
        public static readonly DNSRecordType A = new DNSRecordType("A", "Host address");
        public static readonly DNSRecordType AAAA = new DNSRecordType("AAAA", "IPv6 host address");
        public static readonly DNSRecordType AFSDB = new DNSRecordType("AFSDB", "AFS Data Base location");
        public static readonly DNSRecordType ALIAS = new DNSRecordType("ALIAS", "Auto resolved alias");
        public static readonly DNSRecordType ATMA = new DNSRecordType("ATMA", "Asynchronous Transfer Mode address");
        public static readonly DNSRecordType CAA = new DNSRecordType("CAA", "Certification Authority Authorization");
        public static readonly DNSRecordType CERT = new DNSRecordType("CERT", "Certificate / CRL");
        public static readonly DNSRecordType CNAME = new DNSRecordType("CNAME", "Canonical name for an alias");
        public static readonly DNSRecordType DHCID = new DNSRecordType("DHCID", "DHCP Information");
        public static readonly DNSRecordType DNAME = new DNSRecordType("DNAME", "Non-Terminal DNS Name Redirection");
        public static readonly DNSRecordType DNSKEY = new DNSRecordType("", "DNSSEC public key");
        public static readonly DNSRecordType DS = new DNSRecordType("DS", "Delegation Signer");
        public static readonly DNSRecordType HINFO = new DNSRecordType("HINFO", "Host information");
        public static readonly DNSRecordType ISDN = new DNSRecordType("ISDN", "ISDN address");
        public static readonly DNSRecordType LOC = new DNSRecordType("LOC", "Location information");
        public static readonly DNSRecordType MB = new DNSRecordType("MB", "Mailbox");
        public static readonly DNSRecordType MG = new DNSRecordType("MG", "Mail group member");
        public static readonly DNSRecordType MINFO = new DNSRecordType("MINFO", "Mailbox or mail list information");
        public static readonly DNSRecordType MR = new DNSRecordType("MR", "Renamed mailbox");
        public static readonly DNSRecordType MX = new DNSRecordType("MX", "Mail eXchange");
        public static readonly DNSRecordType NAPTR = new DNSRecordType("NAPTR", "Naming Authority Pointer");
        public static readonly DNSRecordType NS = new DNSRecordType("NS", "Name Server");
        public static readonly DNSRecordType NSAP = new DNSRecordType("NSAP", "NSAP address");
        public static readonly DNSRecordType NSEC = new DNSRecordType("NSEC", "Next Secure");
        public static readonly DNSRecordType NSEC3 = new DNSRecordType("NSEC3", "Next Secure v. 3");
        public static readonly DNSRecordType NSEC3PARAM = new DNSRecordType("NSEC3PARAM", "NSEC3 Parameters");
        public static readonly DNSRecordType PTR = new DNSRecordType("PTR", "Pointer");
        public static readonly DNSRecordType RP = new DNSRecordType("RP", "Responsible person");
        public static readonly DNSRecordType RRSIG = new DNSRecordType("RRSIG", "RRset Signature");
        public static readonly DNSRecordType RT = new DNSRecordType("RT", "Route through");
        public static readonly DNSRecordType SOA = new DNSRecordType("SOA", "Start Of Authority");
        public static readonly DNSRecordType SRV = new DNSRecordType("SRV", "Location of service");
        public static readonly DNSRecordType TLSA = new DNSRecordType("TLSA", "Transport Layer Security Authentication");
        public static readonly DNSRecordType TXT = new DNSRecordType("TXT", "Descriptive text");
        public static readonly DNSRecordType X25 = new DNSRecordType("X25", "X.25 PSDN address");

        public DNSRecordType(string value, string name) : base(name, value)
        {
        }
    }
}
