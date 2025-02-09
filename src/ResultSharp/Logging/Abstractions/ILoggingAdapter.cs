using System.Runtime.CompilerServices;

namespace ResultSharp.Logging.Abstractions
{
    /// <summary>
    /// Provides methods for logging results.
    /// </summary>
    public interface ILoggingAdapter
    {
        /// <summary>
        /// Logs the specified message with the given log level and optional arguments.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        public void Log(string message, LogLevel logLevel, string context, params object?[] args);
    }
}
