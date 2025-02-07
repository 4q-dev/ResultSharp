using ResultSharp.Astractions;
using ResultSharp.Errors.Enums;
namespace ResultSharp.Errors
{
    /// <summary>  
    /// Represents an error with a message and an error code.  
    /// </summary>  
    public record Error : IError
    {
        /// <summary>  
        /// Gets the error code.  
        /// </summary>  
        public ErrorCodes ErrorCode { get; private init; }

        /// <summary>  
        /// Gets the error message.  
        /// </summary>  
        public string Message { get; private init; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="Error"/> record.  
        /// </summary>  
        /// <param name="message">The error message.</param>  
        /// <param name="errorCode">The error code. Default is <see cref="ErrorCodes.Failure"/>.</param>  
        public Error(string message, ErrorCodes errorCode = ErrorCodes.Failure)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        /// <summary>  
        /// Creates a failure error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is "Operation failure."</param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Failure(string message = "Operation failure.")
            => new(message);

        /// <summary>  
        /// Creates a not found error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is "The requested resource was not found."</param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error NotFound(string message = "The requested resource was not found.")
            => new(message, ErrorCodes.NotFound);

        /// <summary>  
        /// Creates a creation error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is "Failed to create a resource."</param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Creation(string message = "Failed to create a resource.")
            => new(message, ErrorCodes.Creation);

        /// <summary>  
        /// Creates a conflict error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message.</param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Conflict(string message)
            => new(message, ErrorCodes.Conflict);

        /// <summary>  
        /// Creates an unauthorized error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is "The user is not logged in."</param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Unauthorized(string message = "The user is not logged in.")
            => new(message, ErrorCodes.Unauthorized);

        /// <summary>  
        /// Creates a forbidden error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is "Access is denied."</param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Forbidden(string message = "Access is denied.")
            => new(message, ErrorCodes.Forbidden);
    }
}
