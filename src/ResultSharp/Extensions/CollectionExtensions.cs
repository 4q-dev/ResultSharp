namespace ResultSharp.Extensions
{
    public static class CollectionExtensions
    {
        public static Result Merge(this ICollection<Result> results)
            => Result.Merge(results.ToArray());

        public static Result<IReadOnlyCollection<TResult>> Merge<TResult>(this ICollection<Result<TResult>> results)
            => Result<TResult>.Merge(results.ToArray());
    }
}
