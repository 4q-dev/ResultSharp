namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class ThenAsyncExtensions
    {
        public static async Task<Result> ThenAsync(this Task<Result> result, Func<Task<Result>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => r
            };
        }

        public static async Task<Result> ThenAsync(this Task<Result> result, Func<Result> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => next(),
                false => r
            };
        }

        public static async Task<Result<TNew>> ThenAsync<TNew>(this Task<Result> result, Func<Task<Result<TNew>>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => Result<TNew>.Failure(r.Errors)
            };
        }

        public static async Task<Result<TNew>> ThenAsync<TNew>(this Task<Result> result, Func<Result<TNew>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => next(),
                false => Result<TNew>.Failure(r.Errors)
            };
        }

        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<TOld, Task<Result<TNew>>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => await next(r).ConfigureAwait(configureAwait),
                false => Result<TNew>.Failure(r.Errors)
            };
        }

        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<TOld, Result<TNew>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => next(r),
                false => Result<TNew>.Failure(r.Errors)
            };
        }

        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<Task<Result<TNew>>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => Result<TNew>.Failure(r.Errors)
            };
        }

        public static async Task<Result<TNew>> ThenAsync<TOld, TNew>(this Task<Result<TOld>> result, Func<Result<TNew>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => next(),
                false => Result<TNew>.Failure(r.Errors)
            };
        }

        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<TOld, Task<Result>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => await next(r).ConfigureAwait(configureAwait),
                false => Result.Failure(r.Errors)
            };
        }

        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<TOld, Result> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => next(r),
                false => Result.Failure(r.Errors)
            };
        }

        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<Task<Result>> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => await next().ConfigureAwait(configureAwait),
                false => Result.Failure(r.Errors)
            };
        }

        public static async Task<Result> ThenAsync<TOld>(this Task<Result<TOld>> result, Func<Result> next, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => next(),
                false => Result.Failure(r.Errors)
            };
        }
    }
}
