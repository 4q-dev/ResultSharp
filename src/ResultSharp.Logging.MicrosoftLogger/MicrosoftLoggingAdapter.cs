using Microsoft.Extensions.Logging;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Logging.MicrosoftLogger
{
    public class MicrosoftLoggingAdapter(ILogger logger) : ILoggingAdapter
    {
        private readonly ILogger logger = logger;

        public void Log(string message, LogLevel logLevel, string context, params object?[] args)
        {
            using (logger.BeginScope(context))
            logger.Log(ConvertLogLevel(logLevel), message, args);
        }

        private Microsoft.Extensions.Logging.LogLevel ConvertLogLevel(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => Microsoft.Extensions.Logging.LogLevel.Trace,
                LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
                LogLevel.Information => Microsoft.Extensions.Logging.LogLevel.Information,
                LogLevel.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
                LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
                LogLevel.Critical => Microsoft.Extensions.Logging.LogLevel.Critical,
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "Invalid log level.")
            };
        }
    }
}
