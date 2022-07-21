using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Infrastructure.Configuration;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using Flurl.Http.Testing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.ExternalAddresses;

public class ExternalAddressClientTests : TestBase, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly HttpTest _httpTest;

    public ExternalAddressClientTests()
    {
        _httpClient = new HttpClient();
        _httpTest = new HttpTest();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _httpTest.Dispose();
    }

    [Fact]
    public async Task InvalidExternalAddressResponses()
    {
        IOptionsSnapshot<ApplicationConfiguration> options = A.Fake<IOptionsSnapshot<ApplicationConfiguration>>();
        A.CallTo(() => options.Value).Returns(CreateValidApplicationConfiguration());

        IExternalAddressClient client = new ExternalAddressClient(options, _httpClient);
        _httpTest.RespondWith(string.Empty);
        _httpTest.RespondWith("Invalid text");
        _httpTest.RespondWith("Not Found", 404);

        Result<IExternalAddressResponse> response = await client.GetAsync(new CancellationToken());

        Assert.True(response.IsFailed);
        Assert.Null(response.ValueOrDefault);
    }

    [Fact]
    public async Task ValidExternalAddressResponse()
    {
        IOptionsSnapshot<ApplicationConfiguration> options = A.Fake<IOptionsSnapshot<ApplicationConfiguration>>();
        A.CallTo(() => options.Value).Returns(CreateValidApplicationConfiguration());

        IExternalAddressClient client = new ExternalAddressClient(options, _httpClient);
        _httpTest.RespondWith("100.100.100.100");

        Result<IExternalAddressResponse> response = await client.GetAsync(new CancellationToken());

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.ValueOrDefault);
    }

    [Fact]
    public async Task ValidFallbackExternalAddressResponse()
    {
        IOptionsSnapshot<ApplicationConfiguration> options = A.Fake<IOptionsSnapshot<ApplicationConfiguration>>();
        A.CallTo(() => options.Value).Returns(CreateValidFallbackApplicationConfiguration());

        IExternalAddressClient client = new ExternalAddressClient(options, _httpClient);

        _httpTest.RespondWith("Not Found", 404);
        _httpTest.RespondWith("100.100.100.100");

        Result<IExternalAddressResponse> response = await client.GetAsync(new CancellationToken());

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.ValueOrDefault);
    }

    private ApplicationConfiguration CreateValidApplicationConfiguration()
    {
        return new ApplicationConfiguration()
        {
            ExternalAddressProviders = new List<ExternalAddressProvider>()
            {
                new ExternalAddressProvider() { Uri = new Uri("https://test.com/") }
            }
        };
    }

    private ApplicationConfiguration CreateValidFallbackApplicationConfiguration()
    {
        return new ApplicationConfiguration()
        {
            ExternalAddressProviders = new List<ExternalAddressProvider>()
            {
                new ExternalAddressProvider() { Uri = new Uri("https://test.com/") },
                new ExternalAddressProvider() { Uri = new Uri("https://test2.com/") },
                new ExternalAddressProvider() { Uri = new Uri("https://test3.com/") },
            }
        };
    }
}