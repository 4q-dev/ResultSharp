using ResultSharp.Configuration;
using ResultSharp.Configuration.Abstractions;
using ResultSharp.Configuration.Logging;
using System.Reflection;

namespace ResultSharp.Tests.Helpers
{
    internal static class ConfigurationHelpers
    {
        internal static void ResetConfiguration(Type type)
        {
            type.GetField("options", BindingFlags.NonPublic | BindingFlags.Static)!
                .SetValue(null, null);

            type.GetProperty("IsConfigured", BindingFlags.Public | BindingFlags.Static)!
                .SetValue(null, false);
        }

        internal static void ResetGloabalConfiguration()
        {
            ResetConfiguration(typeof(ResultConfigurationGlobal));
        }
    }
}
