using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FakeItEasy;
using FluentResults;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy
{
    public class GoDaddyAccountProcessorTests
    {
        [Fact]
        public async Task ProcessAsync_AllSucceed_ReturnsSuccessfulResult()
        {
            IGoDaddyDomainProcessor domainProcessor = A.Fake<IGoDaddyDomainProcessor>();
            A.CallTo(() => domainProcessor.ProcessAsync(A<GoDaddyDomain>.Ignored, A<ExternalAddress>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());
            GoDaddyAccountProcessor processor = new GoDaddyAccountProcessor(domainProcessor);

            GoDaddyAccount account = new GoDaddyAccount() { Domains = new List<GoDaddyDomain>() { new GoDaddyDomain(), new GoDaddyDomain() } };
            ExternalAddress externalAddress = new ExternalAddress() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            Result result = await processor.ProcessAsync(account, externalAddress, new CancellationToken());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ProcessAsync_AnyFailReturnsFailureResult()
        {
            GoDaddyDomain domainOne = new GoDaddyDomain();
            GoDaddyDomain domainTwo = new GoDaddyDomain();

            IGoDaddyDomainProcessor domainProcessor = A.Fake<IGoDaddyDomainProcessor>();
            A.CallTo(() => domainProcessor.ProcessAsync(domainOne, A<ExternalAddress>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());
            A.CallTo(() => domainProcessor.ProcessAsync(domainTwo, A<ExternalAddress>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("This failed"));
            GoDaddyAccountProcessor processor = new GoDaddyAccountProcessor(domainProcessor);

            GoDaddyAccount account = new GoDaddyAccount() { Domains = new List<GoDaddyDomain>() { domainOne, domainTwo } };
            ExternalAddress externalAddress = new ExternalAddress() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            Result result = await processor.ProcessAsync(account, externalAddress, new CancellationToken());

            Assert.True(result.IsFailed);
        }
    }
}
