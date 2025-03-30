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
        /// Executes the appropriate function based on the result's state and returns a new Result.
        /// </summary>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">Function to execute when the result is successful.</param>
        /// <param name="onFailure">Function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result> MatchAsync(this Task<Result> result, Func<Result> onSuccess, Func<ReadOnlyCollection<Error>, Result> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Executes the appropriate asynchronous function based on the result's state and returns a new Result.
        /// </summary>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">Asynchronous function to execute when the result is successful.</param>
        /// <param name="onFailure">Asynchronous function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result> MatchAsync(this Task<Result> result, Func<Task<Result>> onSuccess, Func<ReadOnlyCollection<Error>, Task<Result>> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.MatchAsync(onSuccess, onFailure, configureAwait);
        }

        /// <summary>
        /// Executes the appropriate asynchronous function based on the result's state and returns a new Result.
        /// </summary>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">Asynchronous function to execute when the result is successful.</param>
        /// <param name="onFailure">Asynchronous function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result> MatchAsync(this Result result, Func<Task<Result>> onSuccess, Func<ReadOnlyCollection<Error>, Task<Result>> onFailure, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await onSuccess().ConfigureAwait(configureAwait);
            else
                return await onFailure(result).ConfigureAwait(configureAwait);
        }

        /// <summary>
        /// Executes the appropriate function based on the result's state and returns a new Result with a different type.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <typeparam name="TNew">The type of the value in the new Result.</typeparam>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">Function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result{TNew}"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result<TNew>> MatchAsync<TResult, TNew>(this Task<Result<TResult>> result, Func<TResult, Result<TNew>> onSuccess, Func<ReadOnlyCollection<Error>, Result<TNew>> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Executes the appropriate asynchronous function based on the result's state and returns a new Result with a different type.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <typeparam name="TNew">The type of the value in the new Result.</typeparam>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">Asynchronous function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Asynchronous function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result{TNew}"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result<TNew>> MatchAsync<TResult, TNew>(this Task<Result<TResult>> result, Func<TResult, Task<Result<TNew>>> onSuccess, Func<ReadOnlyCollection<Error>, Task<Result<TNew>>> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.MatchAsync(onSuccess, onFailure, configureAwait);
        }

        /// <summary>
        /// Executes the appropriate asynchronous function based on the result's state and returns a new Result with a different type.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <typeparam name="TNew">The type of the value in the new Result.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">Asynchronous function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Asynchronous function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result{TNew}"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result<TNew>> MatchAsync<TResult, TNew>(this Result<TResult> result, Func<TResult, Task<Result<TNew>>> onSuccess, Func<ReadOnlyCollection<Error>, Task<Result<TNew>>> onFailure, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await onSuccess(result.Value).ConfigureAwait(configureAwait);
            else
                return await onFailure(result).ConfigureAwait(configureAwait);
        }

        /// <summary>
        /// Executes the appropriate function based on the result's state and transforms a typed Result to a non-typed Result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">Function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result> MatchAsync<TResult>(this Task<Result<TResult>> result, Func<TResult, Result> onSuccess, Func<ReadOnlyCollection<Error>, Result> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Executes the appropriate asynchronous function based on the result's state and transforms a typed Result to a non-typed Result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <param name="result">The task representing the result to match.</param>
        /// <param name="onSuccess">Asynchronous function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Asynchronous function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result> MatchAsync<TResult>(this Task<Result<TResult>> result, Func<TResult, Task<Result>> onSuccess, Func<ReadOnlyCollection<Error>, Task<Result>> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.MatchAsync(onSuccess, onFailure, configureAwait);
        }

        /// <summary>
        /// Executes the appropriate asynchronous function based on the result's state and transforms a typed Result to a non-typed Result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">Asynchronous function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Asynchronous function to execute when the result is a failure, providing access to the error collection.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing a new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static async Task<Result> MatchAsync<TResult>(this Result<TResult> result, Func<TResult, Task<Result>> onSuccess, Func<ReadOnlyCollection<Error>, Task<Result>> onFailure, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await onSuccess(result.Value).ConfigureAwait(configureAwait);
            else
                return await onFailure(result).ConfigureAwait(configureAwait);
        }
    }
}
