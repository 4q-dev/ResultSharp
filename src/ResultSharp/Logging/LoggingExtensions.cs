using ResultSharp.Configuration;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Logging
{
    /// <summary>
    /// Provides extension methods for logging results.
    /// </summary>
    public static class LoggingExtensions
    {
        private static ILoggingAdapter logger = ResultConfigurationGlobal.GetLogger();

        /// <summary>
        /// Logs the result with a trace level message.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogTrace(this Result result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Trace, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a debug level message.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogDebug(this Result result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Debug, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an information level message.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogInformation(this Result result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Information, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a warning level message.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogWarning(this Result result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Warning, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an error level message.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogError(this Result result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Error, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a critical level message.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogCritical(this Result result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Critical, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified message and log level if the result is successful.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogIfSuccess(this Result result, string message = "Operation success", string context = "ResultLogger", LogLevel logLevel = LogLevel.Information, params object?[] args)
        {
            if (result.IsSuccess)
                logger.Log(message, logLevel, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified message and log level if the result is a failure.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result LogIfFailure(this Result result, string message, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error, params object?[] args)
        {
            if (result.IsFailure)
                logger.Log(message, logLevel, context, logLevel, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the summary of error messages and the specified log level if the result is a failure.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public static Result LogIfFailure(this Result result, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error)
        {
            if (result.IsFailure)
            {
                var errorMessage = result.SummaryErrorMessages();
                logger.Log(errorMessage, logLevel, context);
            }

            return result;
        }

        /// <summary>
        /// Logs the result with a trace level message.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogTrace<TResult>(this Result<TResult> result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Trace, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a debug level message.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogDebug<TResult>(this Result<TResult> result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Debug, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an information level message.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogInformation<TResult>(this Result<TResult> result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Information, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a warning level message.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogWarning<TResult>(this Result<TResult> result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Warning, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an error level message.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogError<TResult>(this Result<TResult> result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Error, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a critical level message.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogCritical<TResult>(this Result<TResult> result, string message, string context = "ResultLogger", params object?[] args)
        {
            logger.Log(message, LogLevel.Critical, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified message and log level if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogIfSuccess<TResult>(this Result<TResult> result, string message = "Operation success", string context = "ResultLogger", LogLevel logLevel = LogLevel.Information, params object?[] args)
        {
            if (result.IsSuccess)
                logger.Log(message, logLevel, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified pattern and log level if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="pattern">The log message pattern.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogIfSuccess<TResult>(this Result<TResult> result, string pattern = "{value}", string context = "ResultLogger", LogLevel logLevel = LogLevel.Information)
        {
            if (result.IsSuccess)
                logger.Log(pattern, logLevel, context, result.Value);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified message and log level if the result is a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogIfFailure<TResult>(this Result<TResult> result, string message, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error, params object?[] args)
        {
            if (result.IsFailure)
                logger.Log(message, logLevel, context, logLevel, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the summary of error messages and the specified log level if the result is a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogIfFailure<TResult>(this Result<TResult> result, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error)
        {
            if (result.IsFailure)
            {
                var errorMessage = result.SummaryErrorMessages();
                logger.Log(errorMessage, logLevel, context);
            }

            return result;
        }
    }
}