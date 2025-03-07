namespace ResultSharp.Core
{
    /// <summary>
    /// Contains all Merge functins.
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// Merges multiple results into a single result.
        /// </summary>
        /// <param name="results">The results to merge.</param>
        /// <returns>A merged result containing all errors if any.</returns>
        public static Result Merge(params Result[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToArray();

            return errors.Length != 0 ? Failure(errors) : Success();
        }

        /// <summary>
        /// Merges multiple asynchronous results into a single result.
        /// </summary>
        /// <param name="results">The tasks that return results to merge.</param>
        /// <returns>A merged result containing all errors if any.</returns>
        /// <remarks>
        /// This method awaits all provided tasks and merges their results. If any of the results contain errors, 
        /// the merged result will contain all errors. Otherwise, it will return a successful result.
        /// </remarks>
        public static async Task<Result> MergeAsync(params Task<Result>[] results)
            => Merge(await Task.WhenAll(results));
    }
}
