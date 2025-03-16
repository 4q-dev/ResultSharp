using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Extensions.FunctionalExtensions.Sync;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>  
    /// Provides asynchronous extension methods for ensuring conditions on results.  
    /// </summary>  
    public static class EnsureAsyncExtensions
    {
        /// <summary>  
        /// Ensures that the result satisfies the specified predicate asynchronously.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The task representing the result to check.</param>  
        /// <param name="predicate">The predicate to test the result value.</param>  
        /// <param name="onFailure">The error to return if the predicate is not satisfied. If null, a default failure error is used.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the result of the operation, containing the original result if the predicate is satisfied, or the specified error if not.</returns>  
        public static async Task<Result<TResult>> EnsureAsync<TResult>(this Task<Result<TResult>> result, Predicate<TResult> predicate, Error? onFailure = default, bool configureAwait = true)
        {
            ArgumentNullException.ThrowIfNull(result);
            ArgumentNullException.ThrowIfNull(predicate);

            var r = await result.ConfigureAwait(configureAwait);
            return r.Ensure(predicate, onFailure);
        }

        /// <summary>  
        /// Ensures that the result satisfies the specified asynchronous predicate.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The task representing the result to check.</param>  
        /// <param name="predicate">The asynchronous predicate to test the result value.</param>  
        /// <param name="onFailure">The error to return if the predicate is not satisfied. If null, a default failure error is used.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the result of the operation, containing the original result if the predicate is satisfied, or the specified error if not.</returns>  
        public static async Task<Result<TResult>> EnsureAsync<TResult>(this Task<Result<TResult>> result, Func<TResult, Task<bool>> predicate, Error? onFailure = default, bool configureAwait = true)
        {
            ArgumentNullException.ThrowIfNull(result);
            ArgumentNullException.ThrowIfNull(predicate);

            var r = await result.ConfigureAwait(configureAwait);
            return await r.EnsureAsync(predicate, onFailure, configureAwait);
        }

        /// <summary>  
        /// Ensures that the result satisfies the specified asynchronous predicate.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The result to check.</param>  
        /// <param name="predicate">The asynchronous predicate to test the result value.</param>  
        /// <param name="onFailure">The error to return if the predicate is not satisfied. If null, a default failure error is used.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the result of the operation, containing the original result if the predicate is satisfied, or the specified error if not.</returns>  
        public static async Task<Result<TResult>> EnsureAsync<TResult>(this Result<TResult> result, Func<TResult, Task<bool>> predicate, Error? onFailure = default, bool configureAwait = true)
        {
            ArgumentNullException.ThrowIfNull(result);
            ArgumentNullException.ThrowIfNull(predicate);

            if (!result.IsSuccess)
                return result;

            bool isValid = await predicate(result.Value).ConfigureAwait(configureAwait);
            return isValid ? result : onFailure ?? Error.Failure();
        }
    }
}
