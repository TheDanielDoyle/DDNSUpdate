using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DDNSUpdate.Application;
using DDNSUpdate.Application.Mutations;
using DDNSUpdate.Domain;
using Xunit;

namespace DDNSUpdate.Tests.Application;

public class DNSRecordCollectionMutatorTests
{
    [Fact]
    public void Mutate_WhenZeroMutations_HasNoChange()
    {
        DNSRecordCollection dnsRecords = GetDNSRecords();
        IDNSRecordCollectionMutator mutator = new DNSRecordCollectionMutator();

        DNSRecordCollection mutatedDnsRecords = mutator.Mutate(dnsRecords);

        Assert.Equal(dnsRecords, mutatedDnsRecords, DNSRecordEqualityComparer.Instance);
    }

    [Theory]
    [ClassData(typeof(DNSRecordTypes))]
    public void Mutate_WhenRecordTypeMutation_ReturnsOnlyRecordType(DNSRecordType dnsRecordType)
    {
        DNSRecordCollection dnsRecords = GetDNSRecords();
        IDNSRecordCollectionMutator mutator = new DNSRecordCollectionMutator();

        DNSRecordCollection mudatedDnsRecords = mutator.Mutate(dnsRecords, new WithRecordType(dnsRecordType));

        Assert.DoesNotContain(mudatedDnsRecords, d => d.Type != dnsRecordType);
    }

    [Fact]
    public void Mutate_WhenUpdatedData_ReturnsWithUpdatedData()
    {
        string updatedData = "updated-data";
        DNSRecordCollection dnsRecords = GetDNSRecords();
        IDNSRecordCollectionMutator mutator = new DNSRecordCollectionMutator();

        DNSRecordCollection mudatedDnsRecords = mutator.Mutate(dnsRecords, new WithUpdatedData(updatedData));

        Assert.True(mudatedDnsRecords.All(d => string.Equals(d.Data, updatedData)));
    }

    [Fact]
    public void Mutate_WhenUpdatedIds_ReturnsWithUpdatedIds()
    {
        DNSRecordCollection dnsRecords = GetDNSRecords();
        DNSRecordCollection dnsRecordsWithIds = GetDNSRecordsWithIds();
        IDNSRecordCollectionMutator mutator = new DNSRecordCollectionMutator();

        DNSRecordCollection mudatedDnsRecords = mutator.Mutate(dnsRecords, new WithUpdatedIds(dnsRecordsWithIds));

        Assert.False(mudatedDnsRecords.All(d => string.IsNullOrEmpty(d.Id)));
    }

    private DNSRecordCollection GetDNSRecords()
    {
        return new DNSRecordCollection(
            new DNSRecord
            {
                Data = "test-record1",
                Name = "test-name1",
                Type = DNSRecordType.A
            }, 
            new DNSRecord
            {
                Data = "test-record2",
                Name = "test-name2",
                Type = DNSRecordType.AAAA
            }, 
            new DNSRecord
            {
                Data = "test-record3",
                Name = "test-name3",
                Type = DNSRecordType.SRV
            }, 
            new DNSRecord
            {
                Data = "test-record4",
                Name = "test-name4",
                Type = DNSRecordType.A
            });
    }

    private DNSRecordCollection GetDNSRecordsWithIds()
    {
        return new DNSRecordCollection(
            new DNSRecord
            {
                Data = "test-record1",
                Id = "test-id1",
                Name = "test-name1",
                Type = DNSRecordType.A
            }, 
            new DNSRecord
            {
                Data = "test-record2",
                Id = "test-id2",
                Name = "test-name2",
                Type = DNSRecordType.AAAA
            }, 
            new DNSRecord
            {
                Data = "test-record3",
                Id = "test-id3",
                Name = "test-name3",
                Type = DNSRecordType.SRV
            }, 
            new DNSRecord
            {
                Data = "test-record4",
                Id = "test-id4",
                Name = "test-name4",
                Type = DNSRecordType.A
            });
    }

    private class DNSRecordTypes : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { DNSRecordType.A };
            yield return new object[] { DNSRecordType.AAAA };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}