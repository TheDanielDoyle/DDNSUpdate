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
    public class GoDaddyDNSRecordUpdaterTests : TestBase
    {
        private readonly IMapper _mapper;

        public GoDaddyDNSRecordUpdaterTests()
        {
            _mapper = new MappingHelper(AssembliesUnderTest).Mapper;
        }

        [Fact]
        public async Task No_Records_Are_Successfully_Created_If_Client_Fails()
        {
            IGoDaddyClient client = A.Fake<IGoDaddyClient>();

            A.CallTo(() => client.UpdateDNSRecordAsync(A<GoDaddyUpdateDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("Error"));

            GoDaddyDNSRecordUpdater updater = new(client, _mapper);
            DNSRecordCollection records = new(CreateValidDNSRecord(1), CreateValidDNSRecord(2));
            GoDaddyAuthenticationDetails authicationDetails = new("apiKey", "apiSecret");

            Result result = await updater.UpdateAsync("aDomain.com", records, authicationDetails, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(2, result.Errors.Count);
        }

        [Fact]
        public async Task Returns_Success_When_All_Records_Update()
        {
            IGoDaddyClient client = A.Fake<IGoDaddyClient>();

            A.CallTo(() => client.UpdateDNSRecordAsync(A<GoDaddyUpdateDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            GoDaddyDNSRecordUpdater updater = new(client, _mapper);
            DNSRecordCollection records = new(CreateValidDNSRecord(1), CreateValidDNSRecord(2));
            GoDaddyAuthenticationDetails authicationDetails = new("apiKey", "apiSecret");

            Result result = await updater.UpdateAsync("aDomain.com", records, authicationDetails, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        private DNSRecord CreateValidDNSRecord(int number)
        {
            return new DNSRecord()
            {
                Data = $"recordData{number}",
                Name = $"recordName{number}",
                Port = number,
                Priority = number,
                TTL = number,
                Weight = number,
                Tag = $"Tag{number}",
                Type = DNSRecordType.A
            };
        }
    }
}