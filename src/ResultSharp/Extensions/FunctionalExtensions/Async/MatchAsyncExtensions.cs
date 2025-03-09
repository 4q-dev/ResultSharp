using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Extensions.FunctionalExtensions.Sync;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>
    /// Provides asynchronous extension methods for matching results.
    /// </summary>
    public static class MatchAsyncExtensions
    {
        /// <summary>
        /// Matches the result of a task and executes the appropriate action based on the success or failure of the result.
        /// </summary>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">The action to execute if the result is successful.</param>
        /// <param name="onFailure">The action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result> MatchAsync(this Task<Result> result, Action onSuccess, Action<ReadOnlyCollection<Error>> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Matches the result of a task and executes the appropriate asynchronous function based on the success or failure of the result.
        /// </summary>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result> MatchAsync(this Task<Result> result, Func<Task> onSuccess, Func<ReadOnlyCollection<Error>, Task> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.MatchAsync(onSuccess, onFailure, configureAwait);
        }

        /// <summary>
        /// Matches the result and executes the appropriate asynchronous function based on the success or failure of the result.
        /// </summary>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result> MatchAsync(this Result result, Func<Task> onSuccess, Func<ReadOnlyCollection<Error>, Task> onFailure, bool configureAwait = true)
        {
            if (result.IsSuccess)
                await onSuccess().ConfigureAwait(configureAwait);
            else
                await onFailure(result).ConfigureAwait(configureAwait);

            return result;
        }

        /// <summary>
        /// Matches the result of a task and executes the appropriate action based on the success or failure of the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">The action to execute if the result is successful.</param>
        /// <param name="onFailure">The action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> MatchAsync<TResult>(this Task<Result<TResult>> result, Action<TResult> onSuccess, Action<ReadOnlyCollection<Error>> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Matches the result of a task and executes the appropriate asynchronous function based on the success or failure of the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> MatchAsync<TResult>(this Task<Result<TResult>> result, Func<TResult, Task> onSuccess, Func<ReadOnlyCollection<Error>, Task> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.MatchAsync(onSuccess, onFailure, configureAwait);
        }

        /// <summary>
        /// Matches the result and executes the appropriate asynchronous function based on the success or failure of the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> MatchAsync<TResult>(this Result<TResult> result, Func<TResult, Task> onSuccess, Func<ReadOnlyCollection<Error>, Task> onFailure, bool configureAwait = true)
        {
            if (result.IsSuccess)
                await onSuccess(result.Value).ConfigureAwait(configureAwait);
            else
                await onFailure(result).ConfigureAwait(configureAwait);

            return result;
        }
    }
}
