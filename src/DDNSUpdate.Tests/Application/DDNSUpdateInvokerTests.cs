using DDNSUpdate.Application;
using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application
{
    public class DDNSUpdateInvokerTests : TestBase
    {
        [Fact]
        public async Task SingleExceptionIsCaught()
        {
            IEnumerable<IDDNSService> services = new[] { new NotImplementedExceptionThrowingDDNSService() };
            IConfigurationValidator configurationValidator = A.Fake<IConfigurationValidator>();
            IExternalAddressClient externalAddressClient = A.Fake<IExternalAddressClient>();

            A.CallTo(() => configurationValidator.ValidateAsync(A<CancellationToken>.Ignored)).Returns(Result.Ok());

            A.CallTo(() => externalAddressClient.GetAsync(A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(Result.Ok<IExternalAddressResponse>(new ExternalAddressResponse(default))));

            IServiceProvider scopeServiceProvider = A.Fake<IServiceProvider>();
            A.CallTo(() => scopeServiceProvider.GetService(typeof(IEnumerable<IDDNSService>))).Returns(services);

            IServiceScope fakeScope = A.Fake<IServiceScope>();
            A.CallTo(() => fakeScope.ServiceProvider).Returns(scopeServiceProvider);
            A.CallTo(() => scopeServiceProvider.GetService(typeof(IExternalAddressClient))).Returns(externalAddressClient);

            IScopeBuilder scopeBuilder = A.Fake<IScopeBuilder>();
            A.CallTo(() => scopeBuilder.Build()).Returns(fakeScope);

            IDDNSUpdateInvoker invoker = new DDNSUpdateInvoker(scopeBuilder);
            await Assert.ThrowsAsync<NotImplementedException>(() => invoker.InvokeAsync(new CancellationToken()));
        }

        private class NotImplementedExceptionThrowingDDNSService : IDDNSService
        {
            public Task<Result> ProcessAsync(ExternalAddress externalAddress, CancellationToken cancellation)
            {
                throw new NotImplementedException();
            }
        }
    }
}
