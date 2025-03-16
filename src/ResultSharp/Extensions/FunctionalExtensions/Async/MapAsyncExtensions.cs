using ResultSharp.Core;
using ResultSharp.Extensions.FunctionalExtensions.Sync;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>
    /// Provides asynchronous extension methods for mapping results.
    /// </summary>
    public static class MapAsyncExtensions
    {
        /// <summary>
        /// Maps the result of a task to a new result using the specified mapping function.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="map">The function to map the original result value to the new result value.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the new result of the operation.</returns>
        public static async Task<Result<TNew>> MapAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<TOld, TNew> map, bool configureAwait = true)
        {
            ArgumentNullException.ThrowIfNull(result);
            ArgumentNullException.ThrowIfNull(map);

            var r = await result.ConfigureAwait(configureAwait);
            return r.Map(map);
        }

        /// <summary>
        /// Maps the result of a task to a new result using the specified asynchronous mapping function.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The task representing the original result.</param>
        /// <param name="map">The asynchronous function to map the original result value to the new result value.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the new result of the operation.</returns>
        public static async Task<Result<TNew>> MapAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<TOld, Task<TNew>> map, bool configureAwait = true)
        {
            ArgumentNullException.ThrowIfNull(result);
            ArgumentNullException.ThrowIfNull(map);

            var r = await result.ConfigureAwait(configureAwait);
            if (!r.IsSuccess)
                return Result<TNew>.Failure(r.Errors);

            TNew newValue = await map(r.Value).ConfigureAwait(configureAwait);
            return Result<TNew>.Success(newValue);
        }

        /// <summary>
        /// Maps the result to a new result using the specified asynchronous mapping function.
        /// </summary>
        /// <typeparam name="TOld">The type of the original result value.</typeparam>
        /// <typeparam name="TNew">The type of the new result value.</typeparam>
        /// <param name="result">The original result.</param>
        /// <param name="map">The asynchronous function to map the original result value to the new result value.</param>
        /// <param name="configureAwait">Indicates whether to configure await.</param>
        /// <returns>A task representing the new result of the operation.</returns>
        public static async Task<Result<TNew>> MapAsync<TOld, TNew>(this Result<TOld> result, Func<TOld, Task<TNew>> map, bool configureAwait = true)
        {
            ArgumentNullException.ThrowIfNull(result);
            ArgumentNullException.ThrowIfNull(map);

            if (!result.IsSuccess)
                return Result<TNew>.Failure(result.Errors);

            TNew newValue = await map(result.Value).ConfigureAwait(configureAwait);
            return Result<TNew>.Success(newValue);
        }
    }
}
