using ResultSharp.Astractions;
using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp
{
    public sealed class Result :
        ResultBase<Error>
    {
        private Result(bool isSuccess, params Error[]? errors) : base(isSuccess, errors) { }

        public static Result Success()
            => new(true, default);

        public static Result Failure(params Error[] errors)
            => new(false, errors);
        public static Result Failure(IEnumerable<Error> errors)
            => new(false, errors.ToArray());

        public static Result Merge(params Result[] results)
        {
            var errors = results
                .Where(r => r.IsFailure)
                .SelectMany(r => r.Errors)
                .ToArray();

            return errors.Any() ? Failure(errors) : Success();
        }

        public static Result Try(Action func, Func<Exception, Error> handler)
        {
            try
            {
                func();
                return Success();
            }
            catch (Exception e)
            {
                return handler.Invoke(e);
            }
        }

        public static Result<object> Try(Func<object> func, Func<Exception, Error> handler)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                return handler.Invoke(e);
            }
        }

        public static implicit operator Result(Error error)
            => new(false, error);

        public static implicit operator Result(List<Error> errors)
            => new(false, errors.ToArray());

        public static implicit operator Result(Error[] errors)
            => new(false, errors);

        public static implicit operator ReadOnlyCollection<Error>(Result result)
            => (ReadOnlyCollection<Error>)result.Errors;
    }
}
