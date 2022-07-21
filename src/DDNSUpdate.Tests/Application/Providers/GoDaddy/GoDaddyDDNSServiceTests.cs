using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Application.Providers.GoDaddy.Configuration;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FakeItEasy;
using FluentResults;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy;

public class GoDaddyDDNSServiceTests
{
    [Fact]
    public async Task ProcessAsync_AllAccountsReturnSuccessfulResult_ReturnsSuccessfulResult()
    {
        ExternalAddress externalAddress = new() { IPv4Address = IPAddress.Parse("100.100.100.100") };

        GoDaddyConfiguration config = new() { Accounts = new List<GoDaddyAccount>() { new GoDaddyAccount(), new GoDaddyAccount() } };
        IOptionsSnapshot<GoDaddyConfiguration> optionsSnapshot = A.Fake<IOptionsSnapshot<GoDaddyConfiguration>>();
        IGoDaddyAccountProcessor accountProcessor = A.Fake<IGoDaddyAccountProcessor>();

        A.CallTo(() => optionsSnapshot.Value).Returns(config);
        A.CallTo(() => accountProcessor.ProcessAsync(A<GoDaddyAccount>.Ignored, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Ok());

        GoDaddyDDNSService DOService = new(accountProcessor, optionsSnapshot);

        Result actual = await DOService.ProcessAsync(externalAddress, new CancellationToken());

        Assert.True(actual.IsSuccess);
    }

    [Fact]
    public async Task ProcessAsync_AnyAccountsReturnFailureResult_ReturnsFailureResult()
    {
        ExternalAddress externalAddress = new() { IPv4Address = IPAddress.Parse("100.100.100.100") };

        GoDaddyAccount accountOne = new();
        GoDaddyAccount accountTwo = new();

        GoDaddyConfiguration config = new() { Accounts = new List<GoDaddyAccount>() { accountOne, accountTwo } };
        IOptionsSnapshot<GoDaddyConfiguration> optionsSnapshot = A.Fake<IOptionsSnapshot<GoDaddyConfiguration>>();
        IGoDaddyAccountProcessor accountProcessor = A.Fake<IGoDaddyAccountProcessor>();

        A.CallTo(() => optionsSnapshot.Value).Returns(config);
        A.CallTo(() => accountProcessor.ProcessAsync(accountOne, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Ok());
        A.CallTo(() => accountProcessor.ProcessAsync(accountTwo, externalAddress, A<CancellationToken>.Ignored)).Returns(Result.Fail("This failed"));

        GoDaddyDDNSService DOService = new(accountProcessor, optionsSnapshot);

        Result actual = await DOService.ProcessAsync(externalAddress, new CancellationToken());

        Assert.True(actual.IsFailed);
    }
}