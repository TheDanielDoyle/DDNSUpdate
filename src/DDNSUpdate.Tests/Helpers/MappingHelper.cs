using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDNSUpdate.Tests.Helpers
{
    internal class MappingHelper
    {
        public MappingHelper(params Assembly[] assemblies)
        {
            MapperConfiguration config = new((cfg) =>
            {
                IEnumerable<Type> profileTypes = MappingProfilesFromAssemblies(assemblies);
                foreach (Type profile in profileTypes)
                {
                    cfg.AddProfile(profile);
                }
            });

            Mapper = config.CreateMapper();
            ResolutionContext = CreateResolutionContext(Mapper);
        }

        public IMapper Mapper { get; }

        public ResolutionContext ResolutionContext { get; }

        private ResolutionContext CreateResolutionContext(IMapper mapper)
        {
            Type type = typeof(ResolutionContext);
            ConstructorInfo constructor = type
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .Single(c => c.GetParameters().Length == 1);
            ResolutionContext resolutionContext = (ResolutionContext)constructor.Invoke(new object[] { (Mapper)mapper });
            return resolutionContext;
        }

        private IEnumerable<Type> MappingProfilesFromAssemblies(params Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)));
        }
    }
}
