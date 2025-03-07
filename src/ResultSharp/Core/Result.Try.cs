using ResultSharp.Configuration;
using ResultSharp.Errors;

namespace ResultSharp.Core
{
    public partial class Result
    {
        /// <summary>
        /// Executes the specified action and returns a result based on the outcome.
        /// </summary>
        /// <param name="func">The action to execute.</param>
        /// <param name="handler">The error handler to invoke if an exception occurs.</param>
        /// <returns>A result indicating success or failure.</returns>
        public static Result Try(Action func, Func<Exception, Error>? handler = null)
        {
            handler ??= ResultConfigurationGlobal.GetExceptionHandler();
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

        /// <summary>
        /// Executes the specified asynchronous function and returns a result based on the outcome.
        /// </summary>
        /// <param name="func">The asynchronous function to execute.</param>
        /// <param name="handler">The error handler to invoke if an exception occurs.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a result indicating success or failure.</returns>
        public static async Task<Result> TryAsync(Func<Task> func, Func<Exception, Error>? handler)
        {
            handler ??= ResultConfigurationGlobal.GetExceptionHandler();
            try
            {
                await func();
                return Success();
            }
            catch (Exception e)
            {
                return handler.Invoke(e);
            }
        }

        /// <summary>
        /// Executes the specified function and returns a result based on the outcome.
        /// </summary>
        /// <param name="func">The function to execute.</param>
        /// <param name="handler">The error handler to invoke if an exception occurs.</param>
        /// <returns>A result containing the function's return value or an error.</returns>
        public static Result<TResult> Try<TResult>(Func<TResult> func, Func<Exception, Error>? handler)
        {
            handler ??= ResultConfigurationGlobal.GetExceptionHandler();
            try
            {
                return func();
            }
            catch (Exception e)
            {
                return handler.Invoke(e);
            }
        }

        /// <summary>
        /// Executes the specified asynchronous function and returns a result based on the outcome.
        /// </summary>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="func">The asynchronous function to execute.</param>
        /// <param name="handler">The error handler to invoke if an exception occurs.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a result indicating success or failure.</returns>
        public static async Task<Result<TResult>> TryAsync<TResult>(Func<Task<TResult>> func, Func<Exception, Error>? handler)
        {
            handler ??= ResultConfigurationGlobal.GetExceptionHandler();
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                return handler.Invoke(e);
            }
        }
    }
}
