namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    public static class MapExtensions
    {
        /// <summary>  
        /// Maps the result value to a new value if the result is successful, otherwise returns a failed result.  
        /// </summary>  
        /// <typeparam name="TOld">The type of the original result value.</typeparam>  
        /// <typeparam name="TNew">The type of the new result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="map">The mapping function to apply if the result is successful.</param>  
        /// <returns>The mapped result or a failed result.</returns>  
        public static Result<TNew> Map<TOld, TNew>(this Result<TOld> result, Func<TOld, TNew> map)
            => result.IsSuccess switch
            {
                true => map(result),
                false => Result<TNew>.Failure(result.Errors)
            };
    }
}
