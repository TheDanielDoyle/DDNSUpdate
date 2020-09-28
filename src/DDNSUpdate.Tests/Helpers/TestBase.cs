using DDNSUpdate.Application;
using System.Reflection;

namespace DDNSUpdate.Tests.Helpers
{
    public abstract class TestBase
    {
        protected Assembly[] AssembliesUnderTest
        {
            get
            {
                return new[]
                {
                    typeof(IDDNSService).Assembly
                };
            }
        }
    }
}
