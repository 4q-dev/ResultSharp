using ResultSharp.Astractions;
using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp
{
    /// <summary>  
    /// Represents the result of an operation, containing success status, value, and errors if any.  
    /// </summary>  
    /// <typeparam name="TResult">The type of the result value.</typeparam>  
    public sealed class Result<TResult> : ResultBase<Error>
    {
        private readonly TResult? value;

        /// <summary>  
        /// Gets the value of the result if the operation was successful.  
        /// </summary>  
        /// <exception cref="InvalidOperationException">Thrown when accessing the value if the operation was not successful.</exception>  
        public TResult Value => IsSuccess ? value! : throw new InvalidOperationException($"Value can not be accessed when {nameof(IsSuccess)} is false");

        private Result(params Error[] errors) : base(false, errors) { }
        private Result(TResult result) : base(true, default) => value = result;

        /// <summary>  
        /// Creates a successful result with the specified value.  
        /// </summary>  
        /// <param name="result">The value of the successful result.</param>  
        /// <returns>A successful result containing the specified value.</returns>  
        public static Result<TResult> Success(TResult result) => new(result);

        /// <summary>  
        /// Creates a failed result with the specified errors.  
        /// </summary>  
        /// <param name="errors">The errors associated with the failure.</param>  
        /// <returns>A failed result.</returns>  
        public static Result<TResult> Failure(params Error[] errors) => new(errors);

        /// <summary>  
        /// Creates a failed result with the specified errors.  
        /// </summary>  
        /// <param name="errors">The errors associated with the failure.</param>  
        /// <returns>A failed result.</returns>  
        public static Result<TResult> Failure(IEnumerable<Error> errors) => new(errors.ToArray());

        /// <summary>  
        /// Merges multiple results into a single result.  
        /// </summary>  
        /// <param name="results">The results to merge.</param>  
        /// <returns>A merged result containing all errors if any, or a collection of values if successful.</returns>  
        public static Result<IReadOnlyCollection<TResult>> Merge(params Result<TResult>[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToArray();

            if (errors.Any())
                return new Result<IReadOnlyCollection<TResult>>(errors);

            var values = results.Select(r => r.Value).ToList().AsReadOnly();
            return new Result<IReadOnlyCollection<TResult>>(values);
        }

        /// <summary>  
        /// Merges multiple results into a single result.  
        /// </summary>  
        /// <param name="results">The results to merge.</param>  
        /// <returns>A merged result containing all errors if any.</returns>  
        public static Result Merge(params Result<object>[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToArray();

            return errors.Any() ? Result.Failure(errors) : Result.Success();
        }

        /// <summary>  
        /// Implicitly converts a value to a successful result.  
        /// </summary>  
        /// <param name="result">The value to convert.</param>  
        public static implicit operator Result<TResult>(TResult result) => new(result);

        /// <summary>  
        /// Implicitly converts an error to a failed result.  
        /// </summary>  
        /// <param name="error">The error to convert.</param>  
        public static implicit operator Result<TResult>(Error error) => new(error);

        /// <summary>  
        /// Implicitly converts a list of errors to a failed result.  
        /// </summary>  
        /// <param name="errors">The errors to convert.</param>  
        public static implicit operator Result<TResult>(List<Error> errors) => new(errors.ToArray());

        /// <summary>  
        /// Implicitly converts an array of errors to a failed result.  
        /// </summary>  
        /// <param name="errors">The errors to convert.</param>  
        public static implicit operator Result<TResult>(Error[] errors) => new(errors);

        /// <summary>  
        /// Implicitly converts a result to its value.  
        /// </summary>  
        /// <param name="result">The result to convert.</param>  
        public static implicit operator TResult(Result<TResult> result) => result.Value;

        /// <summary>  
        /// Implicitly converts a result to a read-only collection of errors.  
        /// </summary>  
        /// <param name="result">The result to convert.</param>  
        public static implicit operator ReadOnlyCollection<Error>(Result<TResult> result) => (ReadOnlyCollection<Error>)result.Errors;
    }
}
