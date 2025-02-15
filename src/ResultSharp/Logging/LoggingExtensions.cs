using ResultSharp.Configuration;
using ResultSharp.Core;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Logging
{
    /// <summary>
    /// Provides extension methods for logging results.
    /// </summary>
    public static class LoggingExtensions
    {
        private static ILoggingAdapter logger => ResultConfigurationGlobal.GetLogger();

        private static void Log(string message, LogLevel logLevel, string context, params object?[] args)
        {
            if (ResultConfigurationGlobal.GlobalOptions.EnableLogging)
                logger.Log(message, logLevel, context, args);
        }

        #region Synchronus Result Logging

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
            Log(message, LogLevel.Trace, context, args);
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
            Log(message, LogLevel.Debug, context, args);
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
            Log(message, LogLevel.Information, context, args);
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
            Log(message, LogLevel.Warning, context, args);
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
            Log(message, LogLevel.Error, context, args);
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
            Log(message, LogLevel.Critical, context, args);
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
                Log(message, logLevel, context, args);
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
                Log(message, logLevel, context, logLevel, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the summary of error messages and the specified log level if the result is a failure.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public static Result LogErrorMessages(this Result result, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error)
        {
            if (result.IsFailure)
            {
                var errorMessage = result.SummaryErrorMessages();
                Log(errorMessage, logLevel, context);
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
            Log(message, LogLevel.Trace, context, args);
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
            Log(message, LogLevel.Debug, context, args);
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
            Log(message, LogLevel.Information, context, args);
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
            Log(message, LogLevel.Warning, context, args);
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
            Log(message, LogLevel.Error, context, args);
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
            Log(message, LogLevel.Critical, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified pattern and log level if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message pattern.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The logged result.</returns>
        public static Result<TResult> LogIfSuccess<TResult>(this Result<TResult> result, string message = "{value}", string context = "ResultLogger", LogLevel logLevel = LogLevel.Information, params object?[] args)
        {
            if (result.IsSuccess)
            {
                Log(message, logLevel, context, 
                    args.Length == 0 ? result.Value : args
                );
            }

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
                Log(message, logLevel, context, logLevel, args);
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
        public static Result<TResult> LogErrorMessages<TResult>(this Result<TResult> result, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error)
        {
            if (result.IsFailure)
            {
                var errorMessage = result.SummaryErrorMessages();
                Log(errorMessage, logLevel, context);
            }

            return result;
        }

        #endregion

        #region Asynchronous Result Logging

        /// <summary>
        /// Logs the result with a trace level message asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result> LogTraceAsync(this Task<Result> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Trace, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a debug level message asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result> LogDebugAsync(this Task<Result> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Debug, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an information level message asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result> LogInformationAsync(this Task<Result> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Information, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a warning level message asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result> LogWarningAsync(this Task<Result> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Warning, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an error level message asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result> LogErrorAsync(this Task<Result> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Error, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a critical level message asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result> LogCriticalAsync(this Task<Result> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Critical, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified message and log level if the result is successful asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static async Task<Result> LogIfSuccessAsync(this Task<Result> result, string message = "Operation success", string context = "ResultLogger", LogLevel logLevel = LogLevel.Information, params object?[] args)
        {
            var r = await result;

            if (r.IsSuccess)
                Log(message, logLevel, context, args);
            return r;
        }

        /// <summary>
        /// Logs the result with the specified message and log level if the result is a failure asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static async Task<Result> LogIfFailureAsync(this Task<Result> result, string message, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error, params object?[] args)
        {
            var r = await result;

            if (r.IsFailure)
                Log(message, logLevel, context, logLevel, args);
            return r;
        }

        /// <summary>
        /// Logs the result with the summary of error messages and the specified log level if the result is a failure asynchronously.
        /// </summary>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static async Task<Result> LogErrorMessagesAsync(this Task<Result> result, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error)
        {
            var r = await result;

            if (r.IsFailure)
            {
                var errorMessage = r.SummaryErrorMessages();
                Log(errorMessage, logLevel, context);
            }

            return r;
        }

        /// <summary>
        /// Logs the result with a trace level message asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result<TResult>> LogTraceAsync<TResult>(this Task<Result<TResult>> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Trace, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a debug level message asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result<TResult>> LogDebugAsync<TResult>(this Task<Result<TResult>> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Debug, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an information level message asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result<TResult>> LogInformationAsync<TResult>(this Task<Result<TResult>> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Information, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a warning level message asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result<TResult>> LogWarningAsync<TResult>(this Task<Result<TResult>> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Warning, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with an error level message asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result<TResult>> LogErrorAsync<TResult>(this Task<Result<TResult>> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Error, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with a critical level message asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static Task<Result<TResult>> LogCriticalAsync<TResult>(this Task<Result<TResult>> result, string message, string context = "ResultLogger", params object?[] args)
        {
            Log(message, LogLevel.Critical, context, args);
            return result;
        }

        /// <summary>
        /// Logs the result with the specified pattern and log level if the result is successful asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message pattern.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static async Task<Result<TResult>> LogIfSuccessAsync<TResult>(this Task<Result<TResult>> result, string message = "{value}", string context = "ResultLogger", LogLevel logLevel = LogLevel.Information, params object?[] args)
        {
            var r = await result;

            if (r.IsSuccess)
                Log(message, logLevel, context, args.Length == 0 ? r.Value : args);
            return r;
        }

        /// <summary>
        /// Logs the result with the specified message and log level if the result is a failure asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="args">Optional arguments for the log message.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static async Task<Result<TResult>> LogIfFailureAsync<TResult>(this Task<Result<TResult>> result, string message, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error, params object?[] args)
        {
            var r = await result;

            if (r.IsFailure)
                Log(message, logLevel, context, logLevel, args);
            return r;
        }

        /// <summary>
        /// Logs the result with the summary of error messages and the specified log level if the result is a failure asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task that returns the result to log.</param>
        /// <param name="context">The context in which the log is being made.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The task that represents the asynchronous operation.</returns>
        public static async Task<Result<TResult>> LogErrorMessagesAsync<TResult>(this Task<Result<TResult>> result, string context = "ResultLogger", LogLevel logLevel = LogLevel.Error)
        {
            var r = await result;

            if (r.IsFailure)
            {
                var errorMessage = r.SummaryErrorMessages();
                Log(errorMessage, logLevel, context);
            }

            return r;
        }

        #endregion
    }
}