using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class OnConditionAsyncExtensions
    {
        public static async Task<Result> OnSuccessAsync(this Task<Result> result, Action action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                action();

            return r;
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> result, Func<Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                await action().ConfigureAwait(configureAwait);

            return r;
        }

        public static async Task<Result<TResult>> OnSuccessAsync<TResult>(this Task<Result<TResult>> result, Action<TResult> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                action(r);

            return r;
        }

        public static async Task<Result<TResult>> OnSuccessAsync<TResult>(this Task<Result<TResult>> result, Func<TResult, Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsSuccess)
                await action(r).ConfigureAwait(configureAwait);

            return r;
        }

        public static async Task<Result> OnFailureAsync(this Task<Result> result, Action action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                action();

            return r;
        }

        public static async Task<Result> OnFailureAsync(this Task<Result> result, Func<Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                await action().ConfigureAwait(configureAwait);

            return r;
        }

        public static async Task<Result<TResult>> OnFailureAsync<TResult>(this Task<Result<TResult>> result, Action<ReadOnlyCollection<Error>> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                action(r);

            return r;
        }

        public static async Task<Result<TResult>> OnFailureAsync<TResult>(this Task<Result<TResult>> result, Func<ReadOnlyCollection<Error>, Task> action, bool configureAwait = true)
        {
            var r = await result.ConfigureAwait(configureAwait);
            if (r.IsFailure)
                await action(r).ConfigureAwait(configureAwait);

            return r;
        }
    }
}
