using Microsoft.Extensions.Logging;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Logging.MicrosoftLogger
{
    /// <summary>  
    /// Adapts the Microsoft Logger to the ILoggingAdapter interface.  
    /// </summary>  
    public class MicrosoftLoggingAdapter : ILoggingAdapter
    {
        private readonly ILogger logger;

        /// <summary>  
        /// Initializes a new instance of the <see cref="MicrosoftLoggingAdapter"/> class.  
        /// </summary>  
        /// <param name="logger">The Microsoft logger to adapt.</param>  
        public MicrosoftLoggingAdapter(ILogger logger)
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
            using (logger.BeginScope(context))
            {
                logger.Log(ConvertLogLevel(logLevel), message, args);
            }
        }

        /// <summary>  
        /// Converts the <see cref="LogLevel"/> to <see cref="Microsoft.Extensions.Logging.LogLevel"/>.  
        /// </summary>  
        /// <param name="logLevel">The log level to convert.</param>  
        /// <returns>The corresponding <see cref="Microsoft.Extensions.Logging.LogLevel"/>.</returns>  
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the log level is invalid.</exception>  
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
