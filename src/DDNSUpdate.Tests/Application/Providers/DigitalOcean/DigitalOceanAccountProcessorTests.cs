using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using FakeItEasy;
using FluentResults;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean
{
    public class DigitalOceanAccountProcessorTests
    {
        [Fact]
        public async Task ReturnsSuccessfulResultWhenAllSucceed()
        {
            IDigitalOceanDomainProcessor domainProcessor = A.Fake<IDigitalOceanDomainProcessor>();
            A.CallTo(() => domainProcessor.ProcessAsync(A<DigitalOceanDomain>.Ignored, A<Domain.ExternalAddress>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());
            DigitalOceanAccountProcessor processor = new DigitalOceanAccountProcessor(domainProcessor);

            DigitalOceanAccount account = new DigitalOceanAccount() { Domains = new List<DigitalOceanDomain>() { new DigitalOceanDomain(), new DigitalOceanDomain() } };
            ExternalAddress externalAddress = new Domain.ExternalAddress() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            Result result = await processor.ProcessAsync(account, externalAddress, new CancellationToken());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ReturnsFailureWhenAnyFail()
        {
            DigitalOceanDomain domainOne = new DigitalOceanDomain();
            DigitalOceanDomain domainTwo = new DigitalOceanDomain();

            IDigitalOceanDomainProcessor domainProcessor = A.Fake<IDigitalOceanDomainProcessor>();
            A.CallTo(() => domainProcessor.ProcessAsync(domainOne, A<Domain.ExternalAddress>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());
            A.CallTo(() => domainProcessor.ProcessAsync(domainTwo, A<Domain.ExternalAddress>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("This failed"));
            DigitalOceanAccountProcessor processor = new DigitalOceanAccountProcessor(domainProcessor);

            DigitalOceanAccount account = new DigitalOceanAccount() { Domains = new List<DigitalOceanDomain>() { domainOne, domainTwo } };
            ExternalAddress externalAddress = new Domain.ExternalAddress() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            Result result = await processor.ProcessAsync(account, externalAddress, new CancellationToken());

            Assert.True(result.IsFailed);
        }
    }
}
