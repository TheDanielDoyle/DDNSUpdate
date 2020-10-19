using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Application.Providers.GoDaddy.Request;
using DDNSUpdate.Domain;
using FakeItEasy;
using FluentResults;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy
{
    public class GoDaddyDNSRecordCreatorTests
    {
        [Fact]
        public async Task No_Records_Are_Successfully_Created_Returns_Failure()
        {
            IGoDaddyClient fakeClient = A.Fake<IGoDaddyClient>();

            A.CallTo(() => fakeClient.CreateDNSRecordAsync(A<GoDaddyCreateDNSRecordRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("oops"));

            GoDaddyDNSRecordCreator creator = new GoDaddyDNSRecordCreator(fakeClient);
            DNSRecordCollection dnsRecords = new DNSRecordCollection(
                new DNSRecord(),
                new DNSRecord()
            );

            Result result = await creator.CreateAsync(string.Empty, dnsRecords, string.Empty, string.Empty, CancellationToken.None);

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Records_Are_Created_And_Successful_Result_Returned()
        {
            IGoDaddyClient fakeClient = A.Fake<IGoDaddyClient>();

            A.CallTo(() => fakeClient.CreateDNSRecordAsync(A<GoDaddyCreateDNSRecordRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            GoDaddyDNSRecordCreator creator = new GoDaddyDNSRecordCreator(fakeClient);
            DNSRecordCollection dnsRecords = new DNSRecordCollection(
                new DNSRecord(),
                new DNSRecord()
            );

            Result result = await creator.CreateAsync(string.Empty, dnsRecords, string.Empty, string.Empty, CancellationToken.None);

            Assert.True(result.IsSuccess);
            A.CallTo(() => fakeClient.CreateDNSRecordAsync(A<GoDaddyCreateDNSRecordRequest>.Ignored, A<CancellationToken>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}