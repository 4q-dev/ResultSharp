namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>  
    /// Provides asynchronous extension methods for handling alternative results.  
    /// </summary>  
    public static class OrElseAsyncExtensions
    {
        /// <summary>  
        /// Returns the original result if it is successful; otherwise, returns the alternative result.  
        /// </summary>  
        /// <param name="result">The task representing the original result.</param>  
        /// <param name="alternative">The function to generate the alternative result if the original result is a failure.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the original result if it is successful; otherwise, the alternative result.</returns>  
        public static async Task<Result> OrElseAsync(this Task<Result> result, Func<Result> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => alternative()
            };
        }

        /// <summary>  
        /// Returns the original result if it is successful; otherwise, returns the alternative result asynchronously.  
        /// </summary>  
        /// <param name="result">The task representing the original result.</param>  
        /// <param name="alternative">The asynchronous function to generate the alternative result if the original result is a failure.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the original result if it is successful; otherwise, the alternative result.</returns>  
        public static async Task<Result> OrElseAsync(this Task<Result> result, Func<Task<Result>> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => await alternative().ConfigureAwait(configureAwait)
            };
        }

        /// <summary>  
        /// Returns the original result if it is successful; otherwise, returns the alternative result.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The task representing the original result.</param>  
        /// <param name="alternative">The function to generate the alternative result if the original result is a failure.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the original result if it is successful; otherwise, the alternative result.</returns>  
        public static async Task<Result<TResult>> OrElseAsync<TResult>(this Task<Result<TResult>> result, Func<Result<TResult>> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => alternative()
            };
        }

        /// <summary>  
        /// Returns the original result if it is successful; otherwise, returns the alternative result asynchronously.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The task representing the original result.</param>  
        /// <param name="alternative">The asynchronous function to generate the alternative result if the original result is a failure.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the original result if it is successful; otherwise, the alternative result.</returns>  
        public static async Task<Result<TResult>> OrElseAsync<TResult>(this Task<Result<TResult>> result, Func<Task<Result<TResult>>> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => await alternative().ConfigureAwait(configureAwait)
            };
        }
    }
}
