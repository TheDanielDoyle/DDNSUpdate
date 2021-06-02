using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean
{
    public class DigitalOceanAccountProcessorTests : TestBase
    {
        [Fact]
        public async Task ReturnsFailureWhenAnyFail()
        {
            DigitalOceanDomain domainOne = new();
            DigitalOceanDomain domainTwo = new();

            IDigitalOceanDomainProcessor domainProcessor = A.Fake<IDigitalOceanDomainProcessor>();
            A.CallTo(() => domainProcessor.ProcessAsync(domainOne, A<ExternalAddress>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());
            A.CallTo(() => domainProcessor.ProcessAsync(domainTwo, A<ExternalAddress>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("This failed"));
            DigitalOceanAccountProcessor processor = new(domainProcessor);

            DigitalOceanAccount account = new() { Domains = new List<DigitalOceanDomain>() { domainOne, domainTwo } };
            ExternalAddress externalAddress = new() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            Result result = await processor.ProcessAsync(account, externalAddress, new CancellationToken());

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task ReturnsSuccessfulResultWhenAllSucceed()
        {
            IDigitalOceanDomainProcessor domainProcessor = A.Fake<IDigitalOceanDomainProcessor>();
            A.CallTo(() => domainProcessor.ProcessAsync(A<DigitalOceanDomain>.Ignored, A<ExternalAddress>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());
            DigitalOceanAccountProcessor processor = new(domainProcessor);

            DigitalOceanAccount account = new() { Domains = new List<DigitalOceanDomain>() { new DigitalOceanDomain(), new DigitalOceanDomain() } };
            ExternalAddress externalAddress = new() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            Result result = await processor.ProcessAsync(account, externalAddress, new CancellationToken());

            Assert.True(result.IsSuccess);
        }
    }
}
