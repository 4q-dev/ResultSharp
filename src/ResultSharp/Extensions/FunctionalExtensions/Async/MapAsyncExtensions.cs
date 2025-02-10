namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class MapAsyncExtensions
    {
        public static async Task<Result<TNew>> MapAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<TOld, TNew> map, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => map(r),
                false => Result<TNew>.Failure(r.Errors)
            };
        }
    }
}
