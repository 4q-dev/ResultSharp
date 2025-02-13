using ResultSharp.Logging.Abstractions;
using Serilog;

namespace ResultSharp.Logging.Serilog
{
    /// <summary>
    /// Adapts the Serilog ILogger to the ILoggingAdapter interface.
    /// </summary>
    public class SerilogAdapter : ILoggingAdapter
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogAdapter"/> class.
        /// </summary>
        /// <param name="logger">The Serilog logger to adapt.</param>
        public SerilogAdapter(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Logs the specified message with the given log level and context.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
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
