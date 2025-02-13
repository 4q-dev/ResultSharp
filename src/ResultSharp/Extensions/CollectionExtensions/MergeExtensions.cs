namespace ResultSharp.Extensions.CollectionExtensions
{
    /// <summary>  
    /// Provides extension methods for collections of Result objects.  
    /// </summary>  
    public static class MergeExtensions
    {
        /// <summary>  
        /// Merges a collection of Result objects into a single Result.  
        /// </summary>  
        /// <param name="results">The collection of Result objects to merge.</param>  
        /// <returns>A merged Result containing all errors if any.</returns>  
        public static Result Merge(this ICollection<Result> results)
            => Result.Merge(results.ToArray());

        /// <summary>  
        /// Merges a collection of Result objects with a specified result type into a single Result.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="results">The collection of Result objects to merge.</param>  
        /// <returns>A merged Result containing all errors if any, or a collection of values if successful.</returns>  
        public static Result<IReadOnlyCollection<TResult>> Merge<TResult>(this ICollection<Result<TResult>> results)
            => Result<TResult>.Merge(results.ToArray());
    }
}
