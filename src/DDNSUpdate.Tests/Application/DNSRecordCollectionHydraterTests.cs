using System.Linq;
using System.Net;
using DDNSUpdate.Application;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSUpdate.Tests.Application
{
    public class DNSRecordCollectionHydraterTests : TestBase
    {
        [Fact]
        public void ReturnsHydratedDNSRecord()
        {
            string ipAddress = "100.100.100.100";
            string id = "124456789";
            DNSRecord dnsRecord = new DNSRecord
            {
                Name = "test",
                TTL = 1800,
                Type = DNSRecordType.A
            };
            DNSRecord dnsRecordToMerge = new DNSRecord
            {
                Id = id,
                Name = "test",
                TTL = 1800,
                Type = DNSRecordType.A
            };
            DNSRecordCollection dnsRecords = new DNSRecordCollection(new[] { dnsRecord });
            DNSRecordCollection dnsRecordsToMerge = new DNSRecordCollection(new[] { dnsRecordToMerge });
            ExternalAddress externalAddress = new ExternalAddress
            {
                IPv4Address = IPAddress.Parse(ipAddress)
            };
            IDNSRecordCollectionHydrater hydrater = new DNSRecordCollectionHydrater();

            DNSRecordCollection hydratedDnsRecords = hydrater.Hydrate(dnsRecords, dnsRecordsToMerge, externalAddress, DNSRecordType.A);

            Assert.NotEmpty(hydratedDnsRecords);

            DNSRecord hydratedDnsRecord = hydratedDnsRecords.First();
            Assert.Equal(ipAddress, hydratedDnsRecord.Data);
            Assert.Equal(dnsRecord.Name, hydratedDnsRecord.Name);
            Assert.Equal(dnsRecord.TTL, hydratedDnsRecord.TTL);
        }
    }
}
