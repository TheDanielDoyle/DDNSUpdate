using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using FakeItEasy;
using FluentResults;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean
{
    public class DigitalOceanDDNSServiceTests
    {
        [Fact]
        public async Task ReturnsSuccessfulIfAllAccountsReturnSuccessful()
        {
            Domain.ExternalAddress externalAddress = new Domain.ExternalAddress() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            DigitalOceanConfiguration config = new DigitalOceanConfiguration() { Accounts = new List<DigitalOceanAccount>() { new DigitalOceanAccount(), new DigitalOceanAccount() } };
            IDigitalOceanAccountProcessor accountProcessor = A.Fake<IDigitalOceanAccountProcessor>();

            A.CallTo(() => accountProcessor.ProcessAsync(A<DigitalOceanAccount>.Ignored, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            DigitalOceanDDNSService DOService = new DigitalOceanDDNSService(config, accountProcessor);

            Result actual = await DOService.ProcessAsync(externalAddress, new CancellationToken());

            Assert.True(actual.IsSuccess);
        }

        [Fact]
        public async Task ReturnsFailureIfAnyAccountsReturnFailure()
        {
            Domain.ExternalAddress externalAddress = new Domain.ExternalAddress() { IPv4Address = IPAddress.Parse("100.100.100.100") };

            DigitalOceanAccount accountOne = new DigitalOceanAccount();
            DigitalOceanAccount accountTwo = new DigitalOceanAccount();

            DigitalOceanConfiguration config = new DigitalOceanConfiguration() { Accounts = new List<DigitalOceanAccount>() { accountOne, accountTwo } };
            IDigitalOceanAccountProcessor accountProcessor = A.Fake<IDigitalOceanAccountProcessor>();

            A.CallTo(() => accountProcessor.ProcessAsync(accountOne, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Ok());
            A.CallTo(() => accountProcessor.ProcessAsync(accountTwo, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Fail("This failed"));

            DigitalOceanDDNSService DOService = new DigitalOceanDDNSService(config, accountProcessor);

            Result actual = await DOService.ProcessAsync(externalAddress, new CancellationToken());

            Assert.True(actual.IsFailed);
        }
    }
}
