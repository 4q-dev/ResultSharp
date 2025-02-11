namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>  
    /// Provides asynchronous extension methods for unwrapping results.  
    /// </summary>  
    public static class UnwrapAsyncExtensions
    {
        /// <summary>  
        /// Unwraps the result of a task, returning the result value if successful, or the specified default value if not.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The task representing the result to unwrap.</param>  
        /// <param name="default">The default value to return if the result is a failure.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the result value if successful, or the specified default value if not.</returns>  
        public static async Task<TResult> UnwrapOrDefaultAsync<TResult>(this Task<Result<TResult>> result, TResult @default, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                return r;

            return @default;
        }

        /// <summary>  
        /// Unwraps the result of a task, returning the result value if successful, or throwing an exception with summary of result errors mesages if not.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The task representing the result to unwrap.</param>  
        /// <param name="configureAwait">Indicates whether to configure await.</param>  
        /// <returns>A task representing the result value if successful.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when the result is a failure.</exception>  
        public static async Task<TResult> UnwrapAsync<TResult>(this Task<Result<TResult>> result, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                return r;

            throw new InvalidOperationException(r.SummaryErrorMessages());
        }
    }
}
