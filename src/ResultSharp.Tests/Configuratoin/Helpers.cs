using System.Reflection;

namespace ResultSharp.Tests.Configuratoin
{
    internal static class Helpers
    {
        internal static void ResetForType(Type type)
        {
            type.GetField("options", BindingFlags.NonPublic | BindingFlags.Static)!
                .SetValue(null, null);

            type.GetProperty("IsConfigured", BindingFlags.Public | BindingFlags.Static)!
                .SetValue(null, false);
        }
    }
}
