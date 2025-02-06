using ResultSharp.Errors;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ResultSharp.Extensions
{
    public static class FunctionExtensions
    {
        public static void Match(this Result result, Action onSuccess, Action onFailure)
        {
            switch (result.IsFailure)
            {
                case true: 
                    onSuccess(); break;
                case false: 
                    onFailure(); break;
            }
        }

        public static void Match<TResult>(this Result<TResult> result, Action<TResult> onSuccess, Action<ReadOnlyCollection<Error>> onFailure)
        {
            switch (result.IsSuccess)
            {
                case true:
                    onSuccess(result); break;
                case false:
                    onFailure(result); break;
            }
        }

        public static object Match(this Result result, Func<object> onSuccess, Func<object> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(),
                false => onFailure()
            };
        }

        public static object Match<TResult>(this Result<TResult> result, Func<TResult, object> onSuccess, Func<ReadOnlyCollection<Error>, object> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(result),
                false => onFailure(result)
            };
        }

        public static Result<TNew> Then<TOld, TNew>(this Result<TOld> result, Func<TOld, Result<TNew>> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(result),
                false => Result<TNew>.Failure(result.Errors)
            };

        public static Result Then<TOld>(this Result<TOld> result, Func<TOld, Result> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(result),
                false => result.Errors.ToArray()
            };

        public static Result Then(this Result result, Func<Result> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(),
                false => result
            };

        public static Result<TNew> Then<TNew>(this Result result, Func<Result<TNew>> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(),
                false => Result<TNew>.Failure(result.Errors)
            };

        public static Result<TNew> Map<TOld, TNew>(this Result<TOld> result, Func<TOld, TNew> map)
            => result.IsSuccess switch
            {
                true => map(result),
                false => Result<TNew>.Failure(result.Errors)
            };

        public static Result Ensure(this Result result, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => result,
                false => onFailure ?? Error.Failure()
            };

        public static Result<TResult> Ensure<TResult>(this Result<TResult> result, Predicate<TResult> predicate, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => predicate(result) ? result : onFailure ?? Error.Failure(),
                false => result
            };

        public static Result<TResult> Ensure<TResult>(this Result<TResult> result, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => result,
                false => onFailure ?? Error.Failure()
            };

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
                action();

            return result;
        }

        public static Result<TResult> OnSuccess<TResult>(this Result<TResult> result, Action<TResult> action)
        {
            if (result.IsSuccess)
                action(result);

            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
                action();

            return result;
        }

        public static Result<TResult> OnFailure<TResult>(this Result<TResult> result, Action<ReadOnlyCollection<Error>> action)
        {
            if (result.IsFailure)
                action(result);

            return result;
        }

        public static TResult UnwrapOrDefault<TResult>(this Result<TResult> result, TResult defualt)
        {
            if (result.IsSuccess)
                return result;

            return defualt;
        }

        public static TResult Unwrap<TResult>(this Result<TResult> result)
        {
            if (result.IsSuccess)
                return result;

            throw new InvalidOperationException(result.SummaryErrorMessages());
        }

        public static Result OrElse(this Result result, Func<Result> alternative)
            => result.IsSuccess switch
            {
                true => result,
                false => alternative()
            };

        public static Result<TResult> OrElse<TResult>(this Result<TResult> result, Func<Result<TResult>> alternative)
            => result.IsSuccess switch
            {
                true => result,
                false => alternative()
            };
    }
}
