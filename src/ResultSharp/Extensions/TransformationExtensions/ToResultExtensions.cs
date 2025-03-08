using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

namespace ResultSharp.Extensions.TransformationExtensions
{

    /// <summary>
    /// Provides extension methods to convert values and tasks to <see cref="Result{T}"/>.
    /// </summary>
    public static class ToResultExtensions
    {
        /// <summary>
        /// Converts the specified value to a <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>A successful result containing the specified value.</returns>
        public static Result<T> ToResult<T>(this T value)
            => Result.Success(value);

        /// <summary>
        /// Converts the specified value to a <see cref="Result{T}"/> based on a set of validation rules.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="ruleSet">A set of validation rules to apply to the value.</param>
        /// <param name="onFailure">Alternative error if any rule fails. Default is Validation Error.</param>
        /// <returns>A result containing the value if all rules pass, or a failure result with errors if any rule fails.</returns>
        public static Result<T> ToResult<T>(this T value, IEnumerable<Predicate<T>> ruleSet, Error? onFailure = default)
        {
            foreach (var rule in ruleSet)
            {
                if (!rule(value))
                {
                    return onFailure ?? Error.Validation();
                }
            }

            return Result.Success(value);
        }

        /// <summary>
        /// Converts the specified task to a <see cref="Result{T}"/> asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The task that returns the value to convert.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a successful result with the specified value.</returns>
        public static async Task<Result<T>> ToResultAsync<T>(this Task<T> value, bool configureAwait = true)
            => Result.Success(await value.ConfigureAwait(configureAwait));

        /// <summary>
        /// Converts the specified task to a <see cref="Result{T}"/> asynchronously based on a set of validation rules.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The task that returns the value to convert.</param>
        /// <param name="ruleSet">A set of validation rules to apply to the value.</param>
        /// <param name="onFailure">Alternative error if any rule fails. Default is Validation Error.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a result with the value if all rules pass, or a failure result with errors if any rule fails.</returns>
        public static async Task<Result<T>> ToResultAsync<T>(this Task<T> value, IEnumerable<Predicate<T>> ruleSet, Error? onFailure = default, bool configureAwait = true)
        {
            var result = await value.ConfigureAwait(configureAwait);
            return result.ToResult(ruleSet, onFailure);
        }
    }
}
