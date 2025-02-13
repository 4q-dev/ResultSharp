using ResultSharp.Errors;

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
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => predicate(r) ? r : onFailure ?? Error.Failure(),
                false => r
            };
        }
    }
}
