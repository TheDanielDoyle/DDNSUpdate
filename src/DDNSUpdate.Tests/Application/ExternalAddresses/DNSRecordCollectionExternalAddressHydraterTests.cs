using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using System.Linq;
using System.Net;
using Xunit;

namespace DDNSUpdate.Tests.Application.ExternalAddresses
{
    public class DNSRecordCollectionExternalAddressHydraterTests : TestBase
    {
        [Fact]
        public void ReturnsHydratedDNSRecord()
        {
            string ipAddress = "100.100.100.100";
            DNSRecord dnsRecord = new DNSRecord
            {
                Name = "test",
                TTL = 1800,
                Type = DNSRecordType.A
            };
            DNSRecordCollection dnsRecords = new DNSRecordCollection(new[] { dnsRecord });
            ExternalAddress externalAddress = new ExternalAddress
            {
                IPv4Address = IPAddress.Parse(ipAddress)
            };
            IDNSRecordCollectionExternalAddressHydrater hydrater = new DNSRecordCollectionExternalAddressHydrater();

            DNSRecordCollection hydratedDnsRecords = hydrater.Hydrate(dnsRecords, externalAddress, DNSRecordType.A);

            Assert.NotEmpty(hydratedDnsRecords);

            DNSRecord hydratedDnsRecord = hydratedDnsRecords.First();
            Assert.Equal(ipAddress, hydratedDnsRecord.Data);
            Assert.Equal(dnsRecord.Name, hydratedDnsRecord.Name);
            Assert.Equal(dnsRecord.TTL, hydratedDnsRecord.TTL);
        }

        [Fact]
        public void ReturnsOriginalDNSRecordCollectionWhenInputEmpty()
        {
            DNSRecordCollection dnsRecords = DNSRecordCollection.Empty;
            ExternalAddress externalAddress = new ExternalAddress
            {
                IPv4Address = IPAddress.None
            };
            IDNSRecordCollectionExternalAddressHydrater hydrater = new DNSRecordCollectionExternalAddressHydrater();
            DNSRecordCollection hydratedDnsRecords = hydrater.Hydrate(dnsRecords, externalAddress, DNSRecordType.A);

            Assert.Same(dnsRecords, hydratedDnsRecords);
        }
    }
}
