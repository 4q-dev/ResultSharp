namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    public static class UnwrapExtensions
    {
        /// <summary>  
        /// Unwraps the result value if successful, otherwise returns a default value.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="defualt">The default value to return if the result is a failure.</param>  
        /// <returns>The result value if successful, otherwise the default value.</returns>  
        public static TResult UnwrapOrDefault<TResult>(this Result<TResult> result, TResult defualt)
        {
            if (result.IsSuccess)
                return result;

            return defualt;
        }

        /// <summary>  
        /// Unwraps the result value if successful, otherwise throws an exception.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <returns>The result value if successful.</returns>  
        /// <exception cref="InvalidOperationException">Thrown if the result is a failure.</exception>  
        public static TResult Unwrap<TResult>(this Result<TResult> result)
        {
            if (result.IsSuccess)
                return result;

            throw new InvalidOperationException(result.SummaryErrorMessages());
        }
    }
}
