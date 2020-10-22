using DDNSUpdate.Application;
using DDNSUpdate.Application.Providers.GoDaddy;
using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using FakeItEasy;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.GoDaddy
{
    public class GoDaddyDomainProcessorTests
    {
        [Fact]
        public async Task ProcessAsync_AllSucceed_ReturnsSuccesfulResult()
        {
            IGoDaddyDNSRecordCreator dnsCreator = A.Fake<IGoDaddyDNSRecordCreator>();
            A.CallTo(() => dnsCreator.CreateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok());

            IDNSRecordCollectionHydrater hydrater = A.Fake<IDNSRecordCollectionHydrater>();
            A.CallTo(() => hydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored))
                .Returns(DNSRecordCollection.Empty());

            IGoDaddyDNSRecordReader dnsReader = A.Fake<IGoDaddyDNSRecordReader>();
            A.CallTo(() => dnsReader.ReadAsync(A<string>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok(DNSRecordCollection.Empty()));

            IGoDaddyDNSRecordUpdater dnsUpdater = A.Fake<IGoDaddyDNSRecordUpdater>();
            A.CallTo(() => dnsUpdater.UpdateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok());

            IGoDaddyDomainProcessor domainProcessor = new GoDaddyDomainProcessor(dnsCreator, hydrater, dnsReader, dnsUpdater);

            GoDaddyDomain domain = new GoDaddyDomain() { Name = "GoDaddy Domain" };
            ExternalAddress externalAddress = A.Fake<ExternalAddress>();
            GoDaddyAuthenticationDetails authenticationDetails = A.Fake<GoDaddyAuthenticationDetails>();

            Result result = await domainProcessor.ProcessAsync(domain, externalAddress, authenticationDetails, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailed);
        }

        [Fact]
        public async Task ProcessAsync_CreateFails_ReturnsFailureResult()
        {
            IGoDaddyDNSRecordCreator dnsCreator = A.Fake<IGoDaddyDNSRecordCreator>();
            A.CallTo(() => dnsCreator.CreateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Fail("Creation Failure"));

            IDNSRecordCollectionHydrater hydrater = A.Fake<IDNSRecordCollectionHydrater>();
            A.CallTo(() => hydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored))
                .Returns(DNSRecordCollection.Empty());

            IGoDaddyDNSRecordReader dnsReader = A.Fake<IGoDaddyDNSRecordReader>();
            A.CallTo(() => dnsReader.ReadAsync(A<string>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok(DNSRecordCollection.Empty()));

            IGoDaddyDNSRecordUpdater dnsUpdater = A.Fake<IGoDaddyDNSRecordUpdater>();
            A.CallTo(() => dnsUpdater.UpdateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok());

            IGoDaddyDomainProcessor domainProcessor = new GoDaddyDomainProcessor(dnsCreator, hydrater, dnsReader, dnsUpdater);

            GoDaddyDomain domain = new GoDaddyDomain() { Name = "GoDaddy Domain" };
            ExternalAddress externalAddress = A.Fake<ExternalAddress>();
            GoDaddyAuthenticationDetails authenticationDetails = A.Fake<GoDaddyAuthenticationDetails>();

            Result result = await domainProcessor.ProcessAsync(domain, externalAddress, authenticationDetails, new CancellationToken());

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task ProcessAsync_ReadFails_ReturnsFailureResult()
        {
            IGoDaddyDNSRecordCreator dnsCreator = A.Fake<IGoDaddyDNSRecordCreator>();
            A.CallTo(() => dnsCreator.CreateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok());

            IDNSRecordCollectionHydrater hydrater = A.Fake<IDNSRecordCollectionHydrater>();
            A.CallTo(() => hydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored))
                .Returns(DNSRecordCollection.Empty());

            IGoDaddyDNSRecordReader dnsReader = A.Fake<IGoDaddyDNSRecordReader>();
            A.CallTo(() => dnsReader.ReadAsync(A<string>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Fail("Reader Failure"));

            IGoDaddyDNSRecordUpdater dnsUpdater = A.Fake<IGoDaddyDNSRecordUpdater>();
            A.CallTo(() => dnsUpdater.UpdateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok());

            IGoDaddyDomainProcessor domainProcessor = new GoDaddyDomainProcessor(dnsCreator, hydrater, dnsReader, dnsUpdater);

            GoDaddyDomain domain = new GoDaddyDomain() { Name = "GoDaddy Domain" };
            ExternalAddress externalAddress = A.Fake<ExternalAddress>();
            GoDaddyAuthenticationDetails authenticationDetails = A.Fake<GoDaddyAuthenticationDetails>();

            Result result = await domainProcessor.ProcessAsync(domain, externalAddress, authenticationDetails, new CancellationToken());

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task ProcessAsync_UpdateFails_ReturnsFailureResult()
        {
            IGoDaddyDNSRecordCreator dnsCreator = A.Fake<IGoDaddyDNSRecordCreator>();
            A.CallTo(() => dnsCreator.CreateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok());

            IDNSRecordCollectionHydrater hydrater = A.Fake<IDNSRecordCollectionHydrater>();
            A.CallTo(() => hydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored))
                .Returns(DNSRecordCollection.Empty());

            IGoDaddyDNSRecordReader dnsReader = A.Fake<IGoDaddyDNSRecordReader>();
            A.CallTo(() => dnsReader.ReadAsync(A<string>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Ok(DNSRecordCollection.Empty()));

            IGoDaddyDNSRecordUpdater dnsUpdater = A.Fake<IGoDaddyDNSRecordUpdater>();
            A.CallTo(() => dnsUpdater.UpdateAsync(A<string>.Ignored, A<DNSRecordCollection>.Ignored, A<GoDaddyAuthenticationDetails>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Result.Fail("Update Failure"));

            IGoDaddyDomainProcessor domainProcessor = new GoDaddyDomainProcessor(dnsCreator, hydrater, dnsReader, dnsUpdater);

            GoDaddyDomain domain = new GoDaddyDomain() { Name = "GoDaddy Domain" };
            ExternalAddress externalAddress = A.Fake<ExternalAddress>();
            GoDaddyAuthenticationDetails authenticationDetails = A.Fake<GoDaddyAuthenticationDetails>();

            Result result = await domainProcessor.ProcessAsync(domain, externalAddress, authenticationDetails, new CancellationToken());

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
        }
    }
}
