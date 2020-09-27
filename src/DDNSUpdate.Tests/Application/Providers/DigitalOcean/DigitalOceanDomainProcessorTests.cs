using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.ExternalAddresses;
using DDNSUpdate.Application.Providers.DigitalOcean;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using FakeItEasy;
using FluentResults;
using Xunit;

namespace DDNSUpdate.Tests.Application.Providers.DigitalOcean
{
    public class DigitalOceanDomainProcessorTests : TestBase
    {
        [Fact]
        public async Task ReturnsSuccessfulResultWhenAllSucceed()
        {
            DNSRecordCollection dnsRecordCollection = DNSRecordCollection.Empty;
            DigitalOceanDomain domain = new DigitalOceanDomain();
            ExternalAddress externalAddress = new ExternalAddress();

            IDNSRecordCollectionExternalAddressHydrater dnsRecordHydrater = A.Fake<IDNSRecordCollectionExternalAddressHydrater>();
            A.CallTo(() => dnsRecordHydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored)).Returns(dnsRecordCollection);

            IDigitalOceanDNSRecordCreator dnsRecordCreator = A.Fake<IDigitalOceanDNSRecordCreator>();
            A.CallTo(() => dnsRecordCreator.CreateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            IDigitalOceanDNSRecordReader dnsRecordReader = A.Fake<IDigitalOceanDNSRecordReader>();
            A.CallTo(() => dnsRecordReader.ReadAsync(A<DigitalOceanDomain>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok(dnsRecordCollection));

            IDigitalOceanDNSRecordUpdater dnsRecordUpdater = A.Fake<IDigitalOceanDNSRecordUpdater>();
            A.CallTo(() => dnsRecordUpdater.UpdateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            IDigitalOceanDomainProcessor processor = new DigitalOceanDomainProcessor(dnsRecordHydrater, dnsRecordCreator, dnsRecordReader, dnsRecordUpdater);
            Result result = await processor.ProcessAsync(domain, externalAddress, string.Empty, new CancellationToken());

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ReturnsFailureWhenReadFails()
        {
            DNSRecordCollection dnsRecordCollection = DNSRecordCollection.Empty;
            DigitalOceanDomain domain = new DigitalOceanDomain();
            ExternalAddress externalAddress = new ExternalAddress();

            IDNSRecordCollectionExternalAddressHydrater dnsRecordHydrater = A.Fake<IDNSRecordCollectionExternalAddressHydrater>();
            A.CallTo(() => dnsRecordHydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored)).Returns(dnsRecordCollection);

            IDigitalOceanDNSRecordCreator dnsRecordCreator = A.Fake<IDigitalOceanDNSRecordCreator>();
            A.CallTo(() => dnsRecordCreator.CreateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            IDigitalOceanDNSRecordReader dnsRecordReader = A.Fake<IDigitalOceanDNSRecordReader>();
            A.CallTo(() => dnsRecordReader.ReadAsync(A<DigitalOceanDomain>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("Error."));

            IDigitalOceanDNSRecordUpdater dnsRecordUpdater = A.Fake<IDigitalOceanDNSRecordUpdater>();
            A.CallTo(() => dnsRecordUpdater.UpdateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            IDigitalOceanDomainProcessor processor = new DigitalOceanDomainProcessor(dnsRecordHydrater, dnsRecordCreator, dnsRecordReader, dnsRecordUpdater);
            Result result = await processor.ProcessAsync(domain, externalAddress, string.Empty, new CancellationToken());

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task ReturnsFailureWhenCreateFails()
        {
            DNSRecordCollection dnsRecordCollection = DNSRecordCollection.Empty;
            DigitalOceanDomain domain = new DigitalOceanDomain();
            ExternalAddress externalAddress = new ExternalAddress();

            IDNSRecordCollectionExternalAddressHydrater dnsRecordHydrater = A.Fake<IDNSRecordCollectionExternalAddressHydrater>();
            A.CallTo(() => dnsRecordHydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored)).Returns(dnsRecordCollection);

            IDigitalOceanDNSRecordCreator dnsRecordCreator = A.Fake<IDigitalOceanDNSRecordCreator>();
            A.CallTo(() => dnsRecordCreator.CreateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("Error."));

            IDigitalOceanDNSRecordReader dnsRecordReader = A.Fake<IDigitalOceanDNSRecordReader>();
            A.CallTo(() => dnsRecordReader.ReadAsync(A<DigitalOceanDomain>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok(dnsRecordCollection));

            IDigitalOceanDNSRecordUpdater dnsRecordUpdater = A.Fake<IDigitalOceanDNSRecordUpdater>();
            A.CallTo(() => dnsRecordUpdater.UpdateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            IDigitalOceanDomainProcessor processor = new DigitalOceanDomainProcessor(dnsRecordHydrater, dnsRecordCreator, dnsRecordReader, dnsRecordUpdater);
            Result result = await processor.ProcessAsync(domain, externalAddress, string.Empty, new CancellationToken());

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task ReturnsFailureWhenUpdateFails()
        {
            DNSRecordCollection dnsRecordCollection = DNSRecordCollection.Empty;
            DigitalOceanDomain domain = new DigitalOceanDomain();
            ExternalAddress externalAddress = new ExternalAddress();

            IDNSRecordCollectionExternalAddressHydrater dnsRecordHydrater = A.Fake<IDNSRecordCollectionExternalAddressHydrater>();
            A.CallTo(() => dnsRecordHydrater.Hydrate(A<DNSRecordCollection>.Ignored, A<ExternalAddress>.Ignored, A<DNSRecordType>.Ignored)).Returns(dnsRecordCollection);

            IDigitalOceanDNSRecordCreator dnsRecordCreator = A.Fake<IDigitalOceanDNSRecordCreator>();
            A.CallTo(() => dnsRecordCreator.CreateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok());

            IDigitalOceanDNSRecordReader dnsRecordReader = A.Fake<IDigitalOceanDNSRecordReader>();
            A.CallTo(() => dnsRecordReader.ReadAsync(A<DigitalOceanDomain>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Ok(dnsRecordCollection));

            IDigitalOceanDNSRecordUpdater dnsRecordUpdater = A.Fake<IDigitalOceanDNSRecordUpdater>();
            A.CallTo(() => dnsRecordUpdater.UpdateAsync(A<DNSRecordCollection>.Ignored, A<string>.Ignored, A<CancellationToken>.Ignored)).Returns(Result.Fail("Error."));

            IDigitalOceanDomainProcessor processor = new DigitalOceanDomainProcessor(dnsRecordHydrater, dnsRecordCreator, dnsRecordReader, dnsRecordUpdater);
            Result result = await processor.ProcessAsync(domain, externalAddress, string.Empty, new CancellationToken());

            Assert.True(result.IsFailed);
        }
    }
}
