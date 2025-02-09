namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    public static class OrElseExtensoins
    {
        /// <summary>  
        /// Returns the original result if successful, otherwise executes an alternative function.  
        /// </summary>  
        /// <param name="result">The original result.</param>  
        /// <param name="alternative">The alternative function to execute if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the result of the alternative function.</returns>  
        public static Result OrElse(this Result result, Func<Result> alternative)
            => result.IsSuccess switch
            {
                true => result,
                false => alternative()
            };

        /// <summary>  
        /// Returns the original result if successful, otherwise executes an alternative function.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="alternative">The alternative function to execute if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the result of the alternative function.</returns>  
        public static Result<TResult> OrElse<TResult>(this Result<TResult> result, Func<Result<TResult>> alternative)
            => result.IsSuccess switch
            {
                true => result,
                false => alternative()
            };
    }
}
