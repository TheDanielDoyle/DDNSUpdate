﻿using DDNSUpdate.Domain;
using DDNSUpdate.Tests.Helpers;
using Xunit;

namespace DDNSUpdate.Tests.Domain;

public class DNSRecordCollectionTests : TestBase
{
    [Fact]
    public void NewRecords()
    {
        DNSRecord record1 = new() { Data = "192.168.10.1", Name = "record1", Type = DNSRecordType.A };
        DNSRecord record2 = new() { Data = "192.168.10.2", Name = "record2", Type = DNSRecordType.A };
        DNSRecord record3 = new() { Data = "192.168.10.1", Name = "record1", Type = DNSRecordType.A };
        DNSRecord record4 = new() { Data = "192.168.10.2", Name = "record2", Type = DNSRecordType.A };
        DNSRecord record5 = new() { Data = "192.168.10.5", Name = "record5", Type = DNSRecordType.A };
        DNSRecord record6 = new() { Data = "192.168.10.6", Name = "record6", Type = DNSRecordType.A };

        DNSRecordCollection compareWith = new(new[]
        {
            record1, record2
        });

        DNSRecordCollection compareTo = new(new[]
        {
            record3, record4, record5, record6
        });

        DNSRecordCollection newRecords = compareWith.WhereNew(compareTo);

        Assert.Contains(record5, newRecords, DNSRecordEqualityComparer.Instance);
        Assert.Contains(record6, newRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record1, newRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record2, newRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record3, newRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record4, newRecords, DNSRecordEqualityComparer.Instance);
    }

    [Fact]
    public void UpdatedRecords()
    {
        DNSRecord record1 = new() { Data = "192.168.10.1", Name = "record1", Type = DNSRecordType.A };
        DNSRecord record2 = new() { Data = "192.168.10.2", Name = "record2", Type = DNSRecordType.A };
        DNSRecord record3 = new() { Data = "192.168.10.3", Name = "record1", Type = DNSRecordType.A };
        DNSRecord record4 = new() { Data = "192.168.10.2", Name = "record2", Type = DNSRecordType.A };
        DNSRecord record5 = new() { Data = "192.168.10.5", Name = "record5", Type = DNSRecordType.A };
        DNSRecord record6 = new() { Data = "192.168.10.6", Name = "record6", Type = DNSRecordType.A };

        DNSRecordCollection compareWith = new(record1, record2, record5);

        DNSRecordCollection compareTo = new(record3, record4, record6);

        DNSRecordCollection updateRecords = compareWith.WhereUpdated(compareTo);

        Assert.Contains(record3, updateRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record1, updateRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record2, updateRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record4, updateRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record5, updateRecords, DNSRecordEqualityComparer.Instance);
        Assert.DoesNotContain(record6, updateRecords, DNSRecordEqualityComparer.Instance);
    }
}