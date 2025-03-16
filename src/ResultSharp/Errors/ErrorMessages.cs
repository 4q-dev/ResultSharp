namespace ResultSharp.Errors
{
    /// <summary>
    /// Contains default error messages for the application.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// Message indicating a general failure during an operation.
        /// </summary>
        public const string OperationFailure = "Operation failure.";

        /// <summary>
        /// Message indicating an error due to invalid data or input validation failure.
        /// </summary>
        public const string ValidationError = "Validation error.";

        /// <summary>
        /// Message indicating that a requested resource could not be found.
        /// </summary>
        public const string NotFound = "The requested resource was not found.";

        /// <summary>
        /// Message indicating a failure to create a new resource.
        /// </summary>
        public const string CreationError = "Failed to create a resource.";

        /// <summary>
        /// Message indicating that the user is not authenticated.
        /// </summary>
        public const string Unauthorized = "The user is not logged in.";

        /// <summary>
        /// Message indicating that access to a resource is denied due to insufficient permissions.
        /// </summary>
        public const string Forbidden = "Access is denied.";

        /// <summary>
        /// Message indicating an unexpected server-side error.
        /// </summary>
        public const string InternalServerError = "Internal server error.";

        /// <summary>
        /// Message indicating that the request was malformed or invalid.
        /// </summary>
        public const string BadRequest = "Bad request.";
    }
}
