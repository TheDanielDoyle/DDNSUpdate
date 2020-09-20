using DDNSUpdate.Application;
using DDNSUpdate.Infrastructure;
using FakeItEasy;
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
        public async Task ExceptionsAreCaught()
        {
            IEnumerable<IDDNSService> services = new[] { new ExceptionThrowingDDNSService() };

            IServiceProvider scopeServiceProvider = A.Fake<IServiceProvider>();
            A.CallTo(() => scopeServiceProvider.GetService(typeof(IEnumerable<IDDNSService>))).Returns(services);

            IServiceScope fakeScope = A.Fake<IServiceScope>();
            A.CallTo(() => fakeScope.ServiceProvider).Returns(scopeServiceProvider);

            IScopeBuilder scopeBuilder = A.Fake<IScopeBuilder>();
            A.CallTo(() => scopeBuilder.Build()).Returns(fakeScope);

            DDNSUpdateInvoker invoker = new DDNSUpdateInvoker(scopeBuilder);
            await Assert.ThrowsAsync<NotImplementedException>(() => invoker.InvokeAsync(new CancellationToken()));
        }

        private class ExceptionThrowingDDNSService : IDDNSService
        {
            public Task ProcessAsync(CancellationToken cancellation)
            {
                throw new NotImplementedException();
            }
        }
    }
}