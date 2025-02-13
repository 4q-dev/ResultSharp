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
            return r.IsSuccess switch
            {
                true => map(r),
                false => Result<TNew>.Failure(r.Errors)
            };
        }
    }
}
