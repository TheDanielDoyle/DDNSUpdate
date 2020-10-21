using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy
{
    public class GoDaddyDNSRecordCreatorTests : TestBase
    {
        private readonly IMapper _mapper;

        public GoDaddyDNSRecordCreatorTests()
        {
            _mapper = new MappingHelper(AssembliesUnderTest).Mapper;
        }

        [Fact]
        public async Task No_Records_Are_Successfully_Created_Returns_Failure()
        {
            IGoDaddyClient fakeClient = A.Fake<IGoDaddyClient>();

            A.CallTo(() => fakeClient.CreateDNSRecordsAsync(A<GoDaddyCreateDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("oops"));

            GoDaddyDNSRecordCreator creator = new GoDaddyDNSRecordCreator(fakeClient, _mapper);
            DNSRecordCollection dnsRecords = new DNSRecordCollection(
                new DNSRecord(),
                new DNSRecord()
            );
            GoDaddyAuthenticationDetails authicationDetails = new GoDaddyAuthenticationDetails(string.Empty, string.Empty);
            
            Result result = await creator.CreateAsync(string.Empty, dnsRecords, authicationDetails, CancellationToken.None);

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Records_Are_Created_And_Successful_Result_Returned()
        {
            IGoDaddyClient fakeClient = A.Fake<IGoDaddyClient>();

            A.CallTo(() => fakeClient.CreateDNSRecordsAsync(A<GoDaddyCreateDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            GoDaddyDNSRecordCreator creator = new GoDaddyDNSRecordCreator(fakeClient, _mapper);
            DNSRecordCollection dnsRecords = new DNSRecordCollection(
                new DNSRecord(),
                new DNSRecord()
            );
            GoDaddyAuthenticationDetails authicationDetails = new GoDaddyAuthenticationDetails(string.Empty, string.Empty);

            Result result = await creator.CreateAsync(string.Empty, dnsRecords, authicationDetails, CancellationToken.None);

            Assert.True(result.IsSuccess);
            A.CallTo(() => fakeClient.CreateDNSRecordsAsync(A<GoDaddyCreateDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}