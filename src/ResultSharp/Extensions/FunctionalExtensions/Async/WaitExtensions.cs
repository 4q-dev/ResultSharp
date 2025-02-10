using ResultSharp.Errors;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    public static class WaitExtensions
    {
        public static Result AwaitResult(this Task<Result> result)
        {
            try
            {
                return result.GetAwaiter().GetResult();
            }
            catch (TaskCanceledException)
            {
                return Error.Failure("The operation was canceled.");
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }

        public static Result<TResult> AwaitResult<TResult>(this Task<Result<TResult>> result)
        {
            try
            {
                return result.GetAwaiter().GetResult();
            }
            catch (TaskCanceledException)
            {
                return Error.Failure("The operation was canceled.");
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}
