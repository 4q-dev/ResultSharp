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
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => r
            };
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
            return r.IsSuccess switch
            {
                true => next(),
                false => r
            };
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
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => Result<TNew>.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => next(),
                false => Result<TNew>.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => await next(r).ConfigureAwait(configureAwait),
                false => Result<TNew>.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => next(r),
                false => Result<TNew>.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => Result<TNew>.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => next(),
                false => Result<TNew>.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => await next(r).ConfigureAwait(configureAwait),
                false => Result.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => next(r),
                false => Result.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => Result.Failure(r.Errors)
            };
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
            return r.IsSuccess switch
            {
                true => next(),
                false => Result.Failure(r.Errors)
            };
        }
    }
}
