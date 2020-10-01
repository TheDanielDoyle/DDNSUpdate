using System.Diagnostics;
using System.Reflection;

namespace DDNSUpdate.Infrastructure.Hosting
{
    public static class AssemblyHelper
    {
        public static readonly string ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
    }
}
