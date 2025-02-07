using ResultSharp.Astractions;
using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp
{
    /// <summary>
    /// Represents the result of an operation, containing success status and errors if any.
    /// </summary>
    public sealed class Result : ResultBase<Error>
    {
        private Result(bool isSuccess, params Error[]? errors) : base(isSuccess, errors) { }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        /// <returns>A successful result.</returns>
        public static Result Success()
            => new(true, default);

        /// <summary>
        /// Creates a failed result with the specified errors.
        /// </summary>
        /// <param name="errors">The errors associated with the failure.</param>
        /// <returns>A failed result.</returns>
        public static Result Failure(params Error[] errors)
            => new(false, errors);

        /// <summary>
        /// Creates a failed result with the specified errors.
        /// </summary>
        /// <param name="errors">The errors associated with the failure.</param>
        /// <returns>A failed result.</returns>
        public static Result Failure(IEnumerable<Error> errors)
            => new(false, errors.ToArray());

        /// <summary>
        /// Merges multiple results into a single result.
        /// </summary>
        /// <param name="results">The results to merge.</param>
        /// <returns>A merged result containing all errors if any.</returns>
        public static Result Merge(params Result[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToArray();

            return errors.Any() ? Failure(errors) : Success();
        }

        /// <summary>
        /// Executes the specified action and returns a result based on the outcome.
        /// </summary>
        /// <param name="func">The action to execute.</param>
        /// <param name="handler">The error handler to invoke if an exception occurs.</param>
        /// <returns>A result indicating success or failure.</returns>
        public static Result Try(Action func, Func<Exception, Error> handler)
        {
            try
            {
                func();
                return Success();
            }
            catch (Exception e)
            {
                return handler.Invoke(e);
            }
        }

        /// <summary>
        /// Executes the specified function and returns a result based on the outcome.
        /// </summary>
        /// <param name="func">The function to execute.</param>
        /// <param name="handler">The error handler to invoke if an exception occurs.</param>
        /// <returns>A result containing the function's return value or an error.</returns>
        public static Result<object> Try(Func<object> func, Func<Exception, Error> handler)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                return handler.Invoke(e);
            }
        }

        /// <summary>
        /// Implicitly converts an error to a failed result.
        /// </summary>
        /// <param name="error">The error to convert.</param>
        public static implicit operator Result(Error error)
            => new(false, error);

        /// <summary>
        /// Implicitly converts a list of errors to a failed result.
        /// </summary>
        /// <param name="errors">The errors to convert.</param>
        public static implicit operator Result(List<Error> errors)
            => new(false, errors.ToArray());

        /// <summary>
        /// Implicitly converts an array of errors to a failed result.
        /// </summary>
        /// <param name="errors">The errors to convert.</param>
        public static implicit operator Result(Error[] errors)
            => new(false, errors);

        /// <summary>
        /// Implicitly converts a result to a read-only collection of errors.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        public static implicit operator ReadOnlyCollection<Error>(Result result)
            => (ReadOnlyCollection<Error>)result.Errors;
    }
}
