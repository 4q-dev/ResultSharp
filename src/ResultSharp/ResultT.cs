using ResultSharp.Astractions;
using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp
{
    public sealed class Result<TResult> : 
        ResultBase<Error>
    {
        private readonly TResult? value;
        public TResult Value => IsSuccess ? value! : throw new InvalidOperationException($"Value can not be accessed when {nameof(IsSuccess)} is false");

        private Result(params Error[] errors) : base(false, errors) { }
        private Result(TResult result) : base(true, default) 
            => value = result;

        public static Result<TResult> Success(TResult result) 
            => new(result);

        public static Result<TResult> Failure(params Error[] errors) 
            => new(errors);

        public static Result<TResult> Failure(IEnumerable<Error> errors)
            => new(errors.ToArray());

        public static Result<IReadOnlyCollection<TResult>> Merge(params Result<TResult>[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToArray();

            if (errors.Any())
                return new Result<IReadOnlyCollection<TResult>>(errors);

            var values = results.Select(r => r.Value).ToList().AsReadOnly();
            return new Result<IReadOnlyCollection<TResult>>(values);
        }

        public static Result Merge(params Result<object>[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToArray();

            return errors.Any() ? Result.Failure(errors) : Result.Success();
        }

        public static implicit operator Result<TResult>(TResult result) 
            => new(result);

        public static implicit operator Result<TResult>(Error error) 
            => new(error);

        public static implicit operator Result<TResult>(List<Error> errors)
            => new(errors.ToArray());

        public static implicit operator Result<TResult>(Error[] errors)
            => new(errors);

        public static implicit operator TResult(Result<TResult> result)
            => result.Value;

        public static implicit operator ReadOnlyCollection<Error>(Result<TResult> result)
            => (ReadOnlyCollection<Error>)result.Errors;
    }
}
