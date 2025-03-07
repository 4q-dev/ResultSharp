using System.Diagnostics.CodeAnalysis;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

namespace ResultSharp.Configuration.ExceptionHandler
{
    /// <summary>
    /// Options for the exception handler.
    /// </summary>
    public record ExceptionHandlerOptions
    {
        private Func<Exception, Error> exceptionHandler = static exception =>
            new Error(exception.Message, ErrorCode.Failure);

        /// <summary>
        /// The exception handler. Default is to return a failure result with the exception message.
        /// </summary>
        public Func<Exception, Error> ExceptionHandler
        {
            get => exceptionHandler;
            internal set => exceptionHandler = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
