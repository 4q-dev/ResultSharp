namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class UnwrapAsyncExtensions
    {
        public static async Task<TResult> UnwrapOrDefaultAsync<TResult>(this Task<Result<TResult>> result, TResult @default, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                return r;

            return @default;
        }

        public static async Task<TResult> UnwrapAsync<TResult>(this Task<Result<TResult>> result, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                return r;
            
            throw new InvalidOperationException(r.SummaryErrorMessages());
        }
    }
}
