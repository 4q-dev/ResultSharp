using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    public static class MatchExtensions
    {
        /// <summary>  
        /// Matches the result and executes the corresponding action based on success or failure.  
        /// </summary>  
        /// <param name="result">The result to match.</param>  
        /// <param name="onSuccess">The action to execute if the result is successful.</param>  
        /// <param name="onFailure">The action to execute if the result is a failure.</param>  
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

        /// <summary>  
        /// Matches the result and executes the corresponding action based on success or failure.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The result to match.</param>  
        /// <param name="onSuccess">The action to execute if the result is successful.</param>  
        /// <param name="onFailure">The action to execute if the result is a failure.</param>  
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

        /// <summary>  
        /// Matches the result and executes the corresponding function based on success or failure, returning the result of the function.  
        /// </summary>  
        /// <param name="result">The result to match.</param>  
        /// <param name="onSuccess">The function to execute if the result is successful.</param>  
        /// <param name="onFailure">The function to execute if the result is a failure.</param>  
        /// <returns>The result of the executed function.</returns>  
        public static object Match(this Result result, Func<object> onSuccess, Func<object> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(),
                false => onFailure()
            };
        }

        /// <summary>  
        /// Matches the result and executes the corresponding function based on success or failure, returning the result of the function.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The result to match.</param>  
        /// <param name="onSuccess">The function to execute if the result is successful.</param>  
        /// <param name="onFailure">The function to execute if the result is a failure.</param>  
        /// <returns>The result of the executed function.</returns>  
        public static object Match<TResult>(this Result<TResult> result, Func<TResult, object> onSuccess, Func<ReadOnlyCollection<Error>, object> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(result),
                false => onFailure(result)
            };
        }
    }
}
