using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Requests;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean
{
    public class DigitalOceanDNSRecordCreatorTests : TestBase
    {
        private readonly MappingHelper _mappingHelper;

        public DigitalOceanDNSRecordCreatorTests()
        {
            _mappingHelper = new MappingHelper(AssembliesUnderTest);
        }

        [Fact]
        public async Task No_Records_Are_Successfully_Created()
        {
            DNSRecordCollection dnsRecords = new DNSRecordCollection(new[]
            {
                new DNSRecord
                {
                    Data = "100.100.100.100",
                    TTL = 1800,
                    Type = DNSRecordType.A
                },
                new DNSRecord
                {
                    Data = "100.100.100.101",
                    TTL = 1800,
                    Type = DNSRecordType.A
                },
            });

            IDigitalOceanClient client = A.Fake<IDigitalOceanClient>();
            A.CallTo(() => client.CreateDNSRecordAsync(A<string>.Ignored, A<DigitalOceanCreateDomainRecordRequest>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("Error"));

            IDigitalOceanDNSRecordCreator creater = new DigitalOceanDNSRecordCreator(client, _mappingHelper.Mapper);

            Result result = await creater.CreateAsync(string.Empty, dnsRecords, string.Empty, CancellationToken.None);

            A.CallTo(() => client.CreateDNSRecordAsync(A<string>.Ignored, A<DigitalOceanCreateDomainRecordRequest>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappenedTwiceExactly();
            Assert.True(result.IsFailed);
            Assert.True(result.Errors.Count == 2);
        }

        [Fact]
        public async Task Two_Records_Are_Successfully_Created()
        {
            DNSRecordCollection dnsRecords = new DNSRecordCollection(new[]
            {
                new DNSRecord
                {
                    Data = "100.100.100.100",
                    TTL = 1800,
                    Type = DNSRecordType.A
                },
                new DNSRecord
                {
                    Data = "100.100.100.101",
                    TTL = 1800,
                    Type = DNSRecordType.A
                },
            });

            IDigitalOceanClient client = A.Fake<IDigitalOceanClient>();
            IDigitalOceanDNSRecordCreator creater = new DigitalOceanDNSRecordCreator(client, _mappingHelper.Mapper);

            Result result = await creater.CreateAsync(string.Empty, dnsRecords, string.Empty, CancellationToken.None);

            A.CallTo(() => client.CreateDNSRecordAsync(A<string>.Ignored, A<DigitalOceanCreateDomainRecordRequest>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappenedTwiceExactly();
            Assert.True(result.IsSuccess);
        }
    }
}
