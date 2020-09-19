using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application;
using DDNSUpdate.Infrastructure.Configuration;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Xunit;

namespace DDNSUpdate.Tests.Application
{
    public class DDNSUpdateHostedServiceTest
    {

        [Fact]
        public async void CallsUpdateOnAllPassedUpdateServices()
        {
        //Given
        IDDNSUpdateService fakeUpdateService = A.Fake<IDDNSUpdateService>();
        A.CallTo(() => fakeUpdateService.UpdateAsync()).Returns(Task.CompletedTask);
        ApplicationConfiguration config = new ApplicationConfiguration(){ UpdateInterval = TimeSpan.FromSeconds(1) };
        var optionMonitor = A.Fake<IOptionsMonitor<ApplicationConfiguration>>();
        A.CallTo(() => optionMonitor.CurrentValue).Returns(config);
        //When
        DDNSUpdateHostedService updateHostedService = new DDNSUpdateHostedService(optionMonitor, new List<IDDNSUpdateService>(){fakeUpdateService});
        await updateHostedService.StartAsync(new CancellationToken());

        Thread.Sleep(2000);
        await updateHostedService.StopAsync(new CancellationToken());
        //Then
        A.CallTo(() => fakeUpdateService.UpdateAsync()).MustHaveHappened();

        }
    }
}