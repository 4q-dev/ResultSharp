using ResultSharp.Abstractions;
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
        public ErrorCode ErrorCode { get; private init; }

        /// <summary>  
        /// Gets the error message.  
        /// </summary>  
        public string Message { get; private init; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="Error"/> record.  
        /// </summary>  
        /// <param name="message">The error message.</param>  
        /// <param name="errorCode">The error code. Default is <see cref="ErrorCode.Failure"/>.</param>  
        public Error(string message, ErrorCode errorCode = ErrorCode.Failure)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        /// <summary>  
        /// Creates a failure error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.OperationFailure"/></param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Failure(string message = ErrorMessages.OperationFailure)
            => new(message);

        /// <summary>
        /// Creates a validation error with the specified message.
        /// </summary>
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.ValidationError"/></param>
        /// <returns>A new instance of the <see cref="Error"/></returns>
        public static Error Validation(string message = ErrorMessages.ValidationError)
            => new(message, ErrorCode.Validation);

        /// <summary>  
        /// Creates a not found error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.NotFound"/></param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error NotFound(string message = ErrorMessages.NotFound)
            => new(message, ErrorCode.NotFound);

        /// <summary>  
        /// Creates a creation error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.CreationError"/></param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Creation(string message = ErrorMessages.CreationError)
            => new(message, ErrorCode.Creation);

        /// <summary>  
        /// Creates a conflict error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message.</param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Conflict(string message)
            => new(message, ErrorCode.Conflict);

        /// <summary>  
        /// Creates an unauthorized error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.Unauthorized"/></param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Unauthorized(string message = ErrorMessages.Unauthorized)
            => new(message, ErrorCode.Unauthorized);

        /// <summary>  
        /// Creates a forbidden error with the specified message.  
        /// </summary>  
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.Forbidden"/></param>  
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>  
        public static Error Forbidden(string message = ErrorMessages.Forbidden)
            => new(message, ErrorCode.Forbidden);

        /// <summary>  
        /// Creates an internal server error with the specified message.
        /// </summary>
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.InternalServerError"/></param>
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>
        public static Error InternalServerError(string message = ErrorMessages.InternalServerError)
            => new(message, ErrorCode.InternalServerError);

        /// <summary>  
        /// Creates a bad request error with the specified message.
        /// </summary>
        /// <param name="message">The error message. Default is <see cref="ErrorMessages.BadRequest"/></param>
        /// <returns>A new instance of the <see cref="Error"/> record.</returns>
        public static Error BadRequest(string message = ErrorMessages.BadRequest)
            => new(message, ErrorCode.BadRequest);
    }
}
