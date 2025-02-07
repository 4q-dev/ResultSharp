namespace ResultSharp.Astractions
{
    /// <summary>
    /// Represents the result of an operation, containing success status and errors if any.
    /// </summary>
    /// <typeparam name="TError">The type of the error.</typeparam>
    public interface IResult<TError>
        where TError : IError
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public bool IsFailure { get; }

        /// <summary>
        /// Gets the collection of errors associated with the operation.
        /// </summary>
        public IReadOnlyCollection<TError> Errors { get; }
    }
}
