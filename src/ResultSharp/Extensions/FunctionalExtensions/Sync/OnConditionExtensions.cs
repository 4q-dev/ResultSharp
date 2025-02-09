using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    public static class OnConditionExtensions
    {
        /// <summary>  
        /// Executes an action if the result is successful.  
        /// </summary>  
        /// <param name="result">The original result.</param>  
        /// <param name="action">The action to execute if the result is successful.</param>  
        /// <returns>The original result.</returns>  
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
                action();

            return result;
        }

        /// <summary>  
        /// Executes an action if the result is successful.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="action">The action to execute if the result is successful.</param>  
        /// <returns>The original result.</returns>  
        public static Result<TResult> OnSuccess<TResult>(this Result<TResult> result, Action<TResult> action)
        {
            if (result.IsSuccess)
                action(result);

            return result;
        }

        /// <summary>  
        /// Executes an action if the result is a failure.  
        /// </summary>  
        /// <param name="result">The original result.</param>  
        /// <param name="action">The action to execute if the result is a failure.</param>  
        /// <returns>The original result.</returns>  
        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
                action();

            return result;
        }

        /// <summary>  
        /// Executes an action if the result is a failure.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="action">The action to execute if the result is a failure.</param>  
        /// <returns>The original result.</returns>  
        public static Result<TResult> OnFailure<TResult>(this Result<TResult> result, Action<ReadOnlyCollection<Error>> action)
        {
            if (result.IsFailure)
                action(result);

            return result;
        }
    }
}
