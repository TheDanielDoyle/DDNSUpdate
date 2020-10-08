using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DDNSUpdate.Application.Providers.GoDaddy;
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
            var client = A.Fake<IGoDaddyClient>();

            A.CallTo(() => client.UpdateDNSRecordsAsync(A<GoDaddyUpdateDNSRecordsRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("Error"));

            GoDaddyDNSRecordUpdater updater = new GoDaddyDNSRecordUpdater(client, _mapper);
            DNSRecordCollection records = new DNSRecordCollection(new DNSRecord(), new DNSRecord());

            var result = await updater.UpdateAsync("aDomain.com", records, "apiKey", "apiSecret", CancellationToken.None);

            Assert.True(result.IsFailed);
        }
    }
}