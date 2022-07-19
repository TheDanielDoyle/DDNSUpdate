using System;
using System.ComponentModel;

namespace DDNSUpdate.Infrastructure.ComponentModel;

public class SmartEnumDescriptionProvider : TypeDescriptionProvider
{
    public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object? instance)
    {
        return new SmartEnumTypeDescriptor(objectType);
    }
}