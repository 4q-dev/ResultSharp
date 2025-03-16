using ResultSharp.Errors;

namespace ResultSharp.Core
{
    public partial class Result<TResult>
    {
        /// <summary>  
        /// Merges multiple results into a single result.  
        /// </summary>  
        /// <param name="results">The results to merge.</param>  
        /// <returns>A merged result containing all errors if any, or a collection of values if successful.</returns>  
        public static Result<IReadOnlyCollection<TResult>> Merge(params Result<TResult>[] results)
        {
            if (results.Length == 0)
                return Result<IReadOnlyCollection<TResult>>.Success(Array.Empty<TResult>().AsReadOnly());

            List<Error>? errors = null;
            List<TResult>? values = null;

            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    errors ??= [];
                    errors.AddRange(result.Errors);
                }
                else
                {
                    values ??= new List<TResult>(results.Length); 
                    values.Add(result.Value);
                }
            }

            return errors is not null
                ? Result<IReadOnlyCollection<TResult>>.Failure(errors.ToArray()) 
                : Result<IReadOnlyCollection<TResult>>.Success(values?.AsReadOnly() ?? Array.Empty<TResult>().AsReadOnly());
        }

        /// <summary>
        /// Merges multiple asynchronous results into a single result.
        /// </summary>
        /// <param name="results">The tasks that return results to merge.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a merged result with all errors if any, or a collection of values if successful.</returns>
        /// <remarks>
        /// This method awaits all provided tasks and merges their results. If any of the results contain errors, 
        /// the merged result will contain all errors. Otherwise, it will return a successful result with a collection of values.
        /// </remarks>
        public static async Task<Result<IReadOnlyCollection<TResult>>> MergeAsync(params Task<Result<TResult>>[] results)
            => Merge(await  Task.WhenAll(results));
    }
}
