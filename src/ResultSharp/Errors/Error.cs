using ResultSharp.Astractions;
using ResultSharp.Errors.Enums;
namespace ResultSharp.Errors
{
    public record Error :
        IError
    {
        public ErrorCodes ErrorCode { get; private init; }
        public string Message { get; private init; }

        public Error(string message, ErrorCodes errorCode = ErrorCodes.Failure)
        {
            ErrorCode = errorCode;
            Message = message;
        }
        
        public static Error Failure(string message = "Operation failure.")
            => new(message);

        public static Error NotFound(string message = "The requested resource was not found.")
            => new(message, ErrorCodes.NotFound);

        public static Error Creation(string message = "Failed to create a resource.")
            => new(message, ErrorCodes.Creation);

        public static Error Conflict(string message)
            => new(message, ErrorCodes.Conflict);

        public static Error Unauthorized(string message = "The user is not logged in.")
            => new(message, ErrorCodes.Unauthorized);

        public static Error Forbidden(string message = "Access is denied.")
            => new(message, ErrorCodes.Forbidden);
    }
}
