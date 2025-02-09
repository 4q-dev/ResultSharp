namespace ResultSharp.Logging.Abstractions
{
    /// <summary>
    /// Provides methods for logging results.
    /// </summary>
    public interface ILoggingAdapter
    {
        /// <summary>
        /// Logs the specified result with the given message and log level.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public Result Log(Result result, string message, LogLevel logLevel);

        /// <summary>
        /// Logs the specified result with the given message and log level if the result is successful.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public Result LogIfSuccess(Result result, string message, LogLevel logLevel);

        /// <summary>
        /// Logs the specified result with the given message and log level if the result is a failure.
        /// </summary>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public Result LogIfFailure(Result result, string message, LogLevel logLevel);

        /// <summary>
        /// Logs the specified result with the given message and log level.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public Result<TResult> Log<TResult>(Result<TResult> result, string message, LogLevel logLevel);

        /// <summary>
        /// Logs the specified result with the given message and log level if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public Result<TResult> LogIfSuccess<TResult>(Result<TResult> result, string message, LogLevel logLevel);

        /// <summary>
        /// Logs the specified result with the given message and log level if the result is a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The logged result.</returns>
        public Result<TResult> LogIfFailure<TResult>(Result<TResult> result, string message, LogLevel logLevel);
    }
}
