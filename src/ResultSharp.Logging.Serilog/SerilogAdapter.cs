using ResultSharp.Logging.Abstractions;
using Serilog;

namespace ResultSharp.Logging.Serilog
{
    public class SerilogAdapter(ILogger logger) : ILoggingAdapter
    {
        private readonly ILogger logger = logger;

        public void Log(string message, LogLevel logLevel, string context, params object?[] args)
        {
            logger.ForContext("Context", context)
                .Write(ConvertLogLevel(logLevel), message, args);
        }

        private static global::Serilog.Events.LogEventLevel ConvertLogLevel(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => global::Serilog.Events.LogEventLevel.Verbose,
                LogLevel.Debug => global::Serilog.Events.LogEventLevel.Debug,
                LogLevel.Information => global::Serilog.Events.LogEventLevel.Information,
                LogLevel.Warning => global::Serilog.Events.LogEventLevel.Warning,
                LogLevel.Error => global::Serilog.Events.LogEventLevel.Error,
                LogLevel.Critical => global::Serilog.Events.LogEventLevel.Fatal,
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "Invalid log level.")
            };
        }
    }
}
