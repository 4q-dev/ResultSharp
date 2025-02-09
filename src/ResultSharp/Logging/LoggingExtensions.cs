using ResultSharp.Configuration;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Logging
{
    public static class LoggingExtensions
    {
        private static ILoggingAdapter logger = ResultConfigurationGlobal.GetLogger();
    }
}
