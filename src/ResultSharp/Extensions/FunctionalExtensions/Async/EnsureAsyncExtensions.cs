using ResultSharp.Errors;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class EnsureAsyncExtensions
    {
        public static async Task<Result<TResult>> EnsureAsync<TResult>(this Task<Result<TResult>> result, Predicate<TResult> predicate, Error? onFailure = default, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            return r.IsSuccess switch
            {
                true => predicate(r) ? r : onFailure ?? Error.Failure(),
                false => r
            };
        }
    }
}
