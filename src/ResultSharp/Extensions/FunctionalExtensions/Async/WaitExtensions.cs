using ResultSharp.Errors;

namespace ResultSharp.Extensions.FunctionalExtensions.Async
{
    /// <summary>
    /// Provides extension methods for awaiting results of asynchronous operations.
    /// </summary>
    public static class WaitExtensions
    {
        /// <summary>
        /// Awaits the result of an asynchronous operation and returns the result for synchronous continuation.
        /// </summary>
        /// <param name="result">The task representing the asynchronous operation.</param>
        /// <returns>The result of the asynchronous operation.</returns>
        /// <exception cref="TaskCanceledException">Thrown when the task is canceled.</exception>
        /// <exception cref="Exception">Thrown when an exception occurs during the task execution.</exception>
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

        /// <summary>
        /// Awaits the result of an asynchronous operation and returns the result for synchronous continuation.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="result">The task representing the asynchronous operation.</param>
        /// <returns>The result of the asynchronous operation.</returns>
        /// <exception cref="TaskCanceledException">Thrown when the task is canceled.</exception>
        /// <exception cref="Exception">Thrown when an exception occurs during the task execution.</exception>
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
