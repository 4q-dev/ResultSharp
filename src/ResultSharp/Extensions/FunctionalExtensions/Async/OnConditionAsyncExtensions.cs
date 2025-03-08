using ResultSharp.Core;
using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>
    /// Provides asynchronous extension methods for executing actions based on the success or failure of results.
    /// </summary>
    public static class OnConditionAsyncExtensions
    {
        #region OnSuccessAsync

        /// <summary>
        /// Executes the specified action if the result is successful.
        /// </summary>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The action to execute if the result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result> OnSuccessAsync(this Task<Result> result, Action action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                action();

            return r;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is successful.
        /// </summary>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result> OnSuccessAsync(this Task<Result> result, Func<Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                await action().ConfigureAwait(configureAwait);

            return r;
        }

        /// <summary>
        /// Executes the specified action if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The action to execute if the result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnSuccessAsync<TResult>(this Task<Result<TResult>> result, Action<TResult> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                action(r.Value);

            return r;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnSuccessAsync<TResult>(this Task<Result<TResult>> result, Func<TResult, Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                await action(r.Value).ConfigureAwait(configureAwait);

            return r;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnSuccessAsync<TResult>(this Result<TResult> result, Func<TResult, Task> action, bool configureAwait = true)
        {
            if (result.IsSuccess)
                await action(result.Value).ConfigureAwait(configureAwait);
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is successful.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnSuccessAsync<TResult>(this Result<TResult> result, Func<Task> action, bool configureAwait = true)
        {
            if (result.IsFailure)
                await action().ConfigureAwait(configureAwait);
            return result;
        }

        #endregion

        #region OnFailureAsync

        /// <summary>
        /// Executes the specified action if the result is a failure.
        /// </summary>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result> OnFailureAsync(this Task<Result> result, Action action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                action();

            return r;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is a failure.
        /// </summary>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result> OnFailureAsync(this Task<Result> result, Func<Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                await action().ConfigureAwait(configureAwait);

            return r;
        }

        /// <summary>
        /// Executes the specified action if the result is a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnFailureAsync<TResult>(this Task<Result<TResult>> result, Action<ReadOnlyCollection<Error>> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                action(r);

            return r;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task representing the result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnFailureAsync<TResult>(this Task<Result<TResult>> result, Func<ReadOnlyCollection<Error>, Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                await action(r).ConfigureAwait(configureAwait);

            return r;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnFailureAsync<TResult>(this Result<TResult> result, Func<ReadOnlyCollection<Error>, Task> action, bool configureAwait = true)
        {
            if (result.IsFailure)
                await action(result).ConfigureAwait(configureAwait);
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is a failure.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the original result of the operation.</returns>
        public static async Task<Result<TResult>> OnFailureAsync<TResult>(this Result<TResult> result, Func<Task> action, bool configureAwait = true)
        {
            if (result.IsFailure)
                await action().ConfigureAwait(configureAwait);
            return result;
        }

        #endregion
    }
}
