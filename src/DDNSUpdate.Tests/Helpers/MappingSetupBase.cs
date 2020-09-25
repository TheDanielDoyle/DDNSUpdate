using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DDNSUpdate.Application.Providers.DigitalOcean;

namespace DDNSUpdate.Tests.Helpers
{
    public class MappingSetupBase
    {
        public IMapper AutoMapper { get; }

        public ResolutionContext ResolutionContext { get; }

        public MappingSetupBase()
        {
            MapperConfiguration config = new MapperConfiguration((cfg) =>
            {
                cfg.AddProfile<DigitalOceanProfile>();
            });

            AutoMapper = config.CreateMapper();
            ResolutionContext = CreateResolutionContext(AutoMapper);
        }

        private ResolutionContext CreateResolutionContext(IMapper mapper)
        {
            Type type = typeof(ResolutionContext);
            ConstructorInfo constructor = type
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .Single(c => c.GetParameters().Length == 1);
            ResolutionContext resolutionContext = (ResolutionContext)constructor.Invoke(new object[] { (Mapper)mapper });
            return resolutionContext;
        }
    }
}