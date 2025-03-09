using ResultSharp.Core;
using ResultSharp.Extensions.FunctionalExtensions.Sync;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>
    /// Provides asynchronous extension methods for chaining results.
    /// </summary>
    public static class ThenAsyncExtensions
    {
        /// <summary>
        /// Chains the result of a task to another asynchronous operation.
        /// </summary>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync(this Task<Result> result, Func<Task<Result>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.ThenAsync(next, configureAwait);
        }

        /// <summary>
        /// Chains the result to another asynchronous operation.
        /// </summary>
        /// <param name="result">The original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync(this Result result, Func<Task<Result>> next, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await next().ConfigureAwait(configureAwait);
            return result;
        }

        /// <summary>
        /// Chains the result of a task to another operation.
        /// </summary>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync(this Task<Result> result, Func<Result> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Then(next);
        }

        /// <summary>
        /// Chains the result of a task to another asynchronous operation that returns a new result type.
        /// </summary>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TNew>(this Task<Result> result, Func<Task<Result<TNew>>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.ThenAsync(next, configureAwait);
        }

        /// <summary>
        /// Chains the result to another asynchronous operation that returns a new result type.
        /// </summary>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TNew>(this Result result, Func<Task<Result<TNew>>> next, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await next().ConfigureAwait(configureAwait);
            return Result<TNew>.Failure(result.Errors);
        }

        /// <summary>
        /// Chains the result of a task to another operation that returns a new result type.
        /// </summary>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TNew>(this Task<Result> result, Func<Result<TNew>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Then(next);
        }

        /// <summary>
        /// Chains the result of a task to another asynchronous operation that takes the original result value and returns a new result type.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<TOld, Task<Result<TNew>>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.ThenAsync(next, configureAwait);
        }

        /// <summary>
        /// Chains the result to another asynchronous operation that takes the original result value and returns a new result type.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Result<TOld> result, Func<TOld, Task<Result<TNew>>> next, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await next(result.Value).ConfigureAwait(configureAwait);
            return Result<TNew>.Failure(result.Errors);
        }

        /// <summary>
        /// Chains the result of a task to another operation that takes the original result value and returns a new result type.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<TOld, Result<TNew>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Then(next);
        }

        /// <summary>
        /// Chains the result of a task to another asynchronous operation that returns a new result type.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<Task<Result<TNew>>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.ThenAsync(next, configureAwait);
        }

        /// <summary>
        /// Chains the result to another asynchronous operation that returns a new result type.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Result<TOld> result, Func<Task<Result<TNew>>> next, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await next().ConfigureAwait(configureAwait);
            return Result<TNew>.Failure(result.Errors);
        }

        /// <summary>
        /// Chains the result of a task to another operation that returns a new result type.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<Result<TNew>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Then(next);
        }

        /// <summary>
        /// Chains the result of a task to another asynchronous operation.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<TOld, Task<Result>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.ThenAsync(next, configureAwait);
        }

        /// <summary>
        /// Chains the result to another asynchronous operation.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <param name="result">The original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync<TOld>(this Result<TOld> result, Func<TOld, Task<Result>> next, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await next(result.Value).ConfigureAwait(configureAwait);
            return Result.Failure(result.Errors);
        }

        /// <summary>
        /// Chains the result of a task to another operation.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<TOld, Result> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Then(next);
        }

        /// <summary>
        /// Chains the result of a task to another asynchronous operation.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<Task<Result>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return await r.ThenAsync(next, configureAwait);
        }

        /// <summary>
        /// Chains the result to another asynchronous operation.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <param name="result">The original result.</param>
        /// <param name="next">The asynchronous function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync<TOld>(this Result<TOld> result, Func<Task<Result>> next, bool configureAwait = true)
        {
            if (result.IsSuccess)
                return await next().ConfigureAwait(configureAwait);
            return Result.Failure(result.Errors);
        }

        /// <summary>
        /// Chains the result of a task to another operation.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="next">The function to execute if the original result is successful.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the result of the chained operation.</returns>
        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<Result> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.Then(next);
        }
    }
}
