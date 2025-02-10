namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class OrElseAsyncExtensions
    {
        public static async Task<Result> OrElse(this Task<Result> result, Func<Result> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => alternative()
            };
        }

        public static async Task<Result> OrElseAsync(this Task<Result> result, Func<Task<Result>> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => await alternative().ConfigureAwait(configureAwait)
            };
        }

        public static async Task<Result<TResult>> OrElse<TResult>(this Task<Result<TResult>> result, Func<Result<TResult>> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => alternative()
            };
        }

        public static async Task<Result<TResult>> OrElseAsync<TResult>(this Task<Result<TResult>> result, Func<Task<Result<TResult>>> alternative, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => r,
                false => await alternative().ConfigureAwait(configureAwait)
            };
        }
    }
}
