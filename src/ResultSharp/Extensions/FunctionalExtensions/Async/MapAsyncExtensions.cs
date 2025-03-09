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
            var r = await result.ConfigureAwait(configureAwait);
            return await r.MapAsync(map, configureAwait);
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
            return result.IsSuccess switch
            {
                true => await map(result).ConfigureAwait(configureAwait),
                false => Result<TNew>.Failure(result.Errors)
            };
        }
    }
}
