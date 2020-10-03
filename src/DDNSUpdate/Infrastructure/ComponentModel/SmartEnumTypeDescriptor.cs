using System;
using System.ComponentModel;

namespace DDNSUpdate.Infrastructure.ComponentModel
{
    public class SmartEnumTypeDescriptor : CustomTypeDescriptor
    {
        private readonly Type _objectType;

        public SmartEnumTypeDescriptor(Type objectType)
        {
            _objectType = objectType;
        }

        public override TypeConverter? GetConverter()
        {
            Type type = _objectType;
            Type converterType = typeof(SmartEnumTypeConverter<>).MakeGenericType(type);
            TypeConverter? converter = Activator.CreateInstance(converterType) as TypeConverter;
            return converter;
        }
    }
}
