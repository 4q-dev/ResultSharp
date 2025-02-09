using ResultSharp.Errors;

namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    public static class EnsureExtensions
    {
        /// <summary>  
        /// Ensures the result is successful, otherwise returns a specified error.  
        /// </summary>  
        /// <param name="result">The original result.</param>  
        /// <param name="onFailure">The error to return if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the specified error.</returns>  
        public static Result Ensure(this Result result, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => result,
                false => onFailure ?? Error.Failure()
            };

        /// <summary>  
        /// Ensures the result is successful and satisfies a predicate, otherwise returns a specified error.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="predicate">The predicate to satisfy if the result is successful.</param>  
        /// <param name="onFailure">The error to return if the result is a failure or does not satisfy the predicate.</param>  
        /// <returns>The original result if successful and satisfies the predicate, otherwise the specified error.</returns>  
        public static Result<TResult> Ensure<TResult>(this Result<TResult> result, Predicate<TResult> predicate, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => predicate(result) ? result : onFailure ?? Error.Failure(),
                false => result
            };

        /// <summary>  
        /// Ensures the result is successful, otherwise returns a specified error.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="onFailure">The error to return if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the specified error.</returns>  
        public static Result<TResult> Ensure<TResult>(this Result<TResult> result, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => result,
                false => onFailure ?? Error.Failure()
            };
    }
}
