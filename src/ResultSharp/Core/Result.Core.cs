using ResultSharp.Abstractions;
using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Core
{
    // NOTE: This file contains the implementation of the logic for the result object itself. The logic for functions, such as Try and Merge, is moved to partial files.

    /// <summary>
    /// Represents the result of an operation, containing success status and errors if any.
    /// </summary>
    public sealed partial class Result : ResultBase<Error>
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
            => new(false, errors.Length > 0 ? errors : [Error.Failure()]);

        /// <summary>
        /// Creates a failed result with the specified errors.
        /// </summary>
        /// <param name="errors">The errors associated with the failure.</param>
        /// <returns>A failed result.</returns>
        public static Result Failure(IEnumerable<Error> errors)
            => new(false, errors.ToArray());

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
