using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class MatchAsyncExtensions
    {
        public static async Task<Result> MatchAsync(this Task<Result> result, Action onSuccess, Action onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            switch (r.IsSuccess)
            {
                case true: onSuccess(); break;
                case false: onFailure(); break;
            }

            return r;
        }

        public static async Task<Result> MatchAsync(this Task<Result> result, Func<Task> onSuccess, Func<Task> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            switch (r.IsSuccess)
            {
                case true: await onSuccess().ConfigureAwait(configureAwait); break;
                case false: await onFailure().ConfigureAwait(configureAwait); break;
            }

            return r;
        }

        public static async Task<Result<TResult>> MatchAsync<TResult>(this Task<Result<TResult>> result, Action<TResult> onSuccess, Action<ReadOnlyCollection<Error>> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            switch (r.IsSuccess)
            {
                case true: onSuccess(r); break;
                case false: onFailure(r); break;
            }

            return r;
        }

        public static async Task<Result<TResult>> MatchAsync<TResult>(this Task<Result<TResult>> result, Func<TResult, Task> onSuccess, Func<ReadOnlyCollection<Error>, Task> onFailure, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            switch (r.IsSuccess)
            {
                case true: await onSuccess(r).ConfigureAwait(configureAwait); break;
                case false: await onFailure(r).ConfigureAwait(configureAwait); break;
            }

            return r;
        }
    }
}
