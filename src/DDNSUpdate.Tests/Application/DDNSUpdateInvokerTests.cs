using DDNSUpdate.Application;
using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Infrastructure;
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
    public class DDNSUpdateInvokerTests
    {
        [Fact]
        public async Task SingleExceptionIsCaught()
        {
            IEnumerable<IDDNSService> services = new[] { new NotImplementedExceptionThrowingDDNSService() };
            IExternalAddressClient externalAddressClient = A.Fake<IExternalAddressClient>();

            A.CallTo(() => externalAddressClient.GetAsync(A<CancellationToken>._))
                .Returns(Task.FromResult(Result.Ok<IExternalAddressResponse>(new ExternalAddressResponse(default))));

            IServiceProvider scopeServiceProvider = A.Fake<IServiceProvider>();
            A.CallTo(() => scopeServiceProvider.GetService(typeof(IEnumerable<IDDNSService>))).Returns(services);

            IServiceScope fakeScope = A.Fake<IServiceScope>();
            A.CallTo(() => fakeScope.ServiceProvider).Returns(scopeServiceProvider);
            A.CallTo(() => scopeServiceProvider.GetService(typeof(IExternalAddressClient))).Returns(externalAddressClient);

            IScopeBuilder scopeBuilder = A.Fake<IScopeBuilder>();
            A.CallTo(() => scopeBuilder.Build()).Returns(fakeScope);

            DDNSUpdateInvoker invoker = new DDNSUpdateInvoker(FakeLogger<DDNSUpdateInvoker>.Instance, scopeBuilder);
            await Assert.ThrowsAsync<NotImplementedException>(() => invoker.InvokeAsync(new CancellationToken()));
        }

        [Fact]
        public async Task MultipleExceptionsAreCaught()
        {
            IEnumerable<IDDNSService> services = new IDDNSService[] { new NotImplementedExceptionThrowingDDNSService(), new IndexOutOfRangeExceptionThrowingDDNSService() };
            IExternalAddressClient externalAddressClient = A.Fake<IExternalAddressClient>();

            A.CallTo(() => externalAddressClient.GetAsync(A<CancellationToken>._))
                .Returns(Task.FromResult(Result.Ok<IExternalAddressResponse>(new ExternalAddressResponse(default))));

            IServiceProvider scopeServiceProvider = A.Fake<IServiceProvider>();
            A.CallTo(() => scopeServiceProvider.GetService(typeof(IEnumerable<IDDNSService>))).Returns(services);
            A.CallTo(() => scopeServiceProvider.GetService(typeof(IExternalAddressClient))).Returns(externalAddressClient);

            IServiceScope fakeScope = A.Fake<IServiceScope>();
            A.CallTo(() => fakeScope.ServiceProvider).Returns(scopeServiceProvider);

            IScopeBuilder scopeBuilder = A.Fake<IScopeBuilder>();
            A.CallTo(() => scopeBuilder.Build()).Returns(fakeScope);

            DDNSUpdateInvoker invoker = new DDNSUpdateInvoker(FakeLogger<DDNSUpdateInvoker>.Instance, scopeBuilder);
            await Assert.ThrowsAsync<AggregateException>(() => invoker.InvokeAsync(new CancellationToken()));
        }

        private class NotImplementedExceptionThrowingDDNSService : IDDNSService
        {
            public Task ProcessAsync(CancellationToken cancellation)
            {
                throw new NotImplementedException();
            }
        }

        private class IndexOutOfRangeExceptionThrowingDDNSService : IDDNSService
        {
            public Task ProcessAsync(CancellationToken cancellation)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}