using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean;

public class DigitalOceanDDNSServiceTests : TestBase
{
    [Fact]
    public async Task ReturnsFailureIfAnyAccountsReturnFailure()
    {
        ExternalAddress externalAddress = new() { IPv4Address = IPAddress.Parse("100.100.100.100") };

        DigitalOceanAccount accountOne = new();
        DigitalOceanAccount accountTwo = new();

        DigitalOceanConfiguration config = new() { Accounts = new List<DigitalOceanAccount>() { accountOne, accountTwo } };
        IOptionsSnapshot<DigitalOceanConfiguration> optionsSnapshot = A.Fake<IOptionsSnapshot<DigitalOceanConfiguration>>();
        IDigitalOceanAccountProcessor accountProcessor = A.Fake<IDigitalOceanAccountProcessor>();

        A.CallTo(() => optionsSnapshot.Value).Returns(config);
        A.CallTo(() => accountProcessor.ProcessAsync(accountOne, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Ok());
        A.CallTo(() => accountProcessor.ProcessAsync(accountTwo, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Fail("This failed"));

        DigitalOceanDDNSService DOService = new(optionsSnapshot, accountProcessor);

        Result actual = await DOService.ProcessAsync(externalAddress, new CancellationToken());

        Assert.True(actual.IsFailed);
    }

    [Fact]
    public async Task ReturnsSuccessfulIfAllAccountsReturnSuccessful()
    {
        ExternalAddress externalAddress = new() { IPv4Address = IPAddress.Parse("100.100.100.100") };

        DigitalOceanConfiguration config = new() { Accounts = new List<DigitalOceanAccount>() { new DigitalOceanAccount(), new DigitalOceanAccount() } };
        IOptionsSnapshot<DigitalOceanConfiguration> optionsSnapshot = A.Fake<IOptionsSnapshot<DigitalOceanConfiguration>>();
        IDigitalOceanAccountProcessor accountProcessor = A.Fake<IDigitalOceanAccountProcessor>();

        A.CallTo(() => optionsSnapshot.Value).Returns(config);
        A.CallTo(() => accountProcessor.ProcessAsync(A<DigitalOceanAccount>.Ignored, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Ok());

        DigitalOceanDDNSService DOService = new(optionsSnapshot, accountProcessor);

        Result actual = await DOService.ProcessAsync(externalAddress, new CancellationToken());

        Assert.True(actual.IsSuccess);
    }
}