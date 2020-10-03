using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Ardalis.SmartEnum;

namespace DDNSUpdate.Infrastructure.ComponentModel
{
    public class SmartEnumTypeConverter<TEnumeration> : TypeConverter where TEnumeration : SmartEnum<TEnumeration, string>
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (int.TryParse((string)value, out int fromInteger))
            {
                return SmartEnum<TEnumeration, string>.FromValue(fromInteger.ToString());
            }
            return SmartEnum<TEnumeration, string>.FromValue((string)value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            PropertyInfo? valueProperty = value.GetType().GetProperty(nameof(SmartEnum<TEnumeration, string>.Value));
            return valueProperty?.GetValue(value);
        }
    }
}