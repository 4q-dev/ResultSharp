using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    /// <summary>  
    /// Provides extension methods for matching results and executing actions based on success or failure.  
    /// </summary>  
    public static class MatchExtensions
    {
        /// <summary>  
        /// Matches the result and executes the corresponding action based on success or failure.  
        /// </summary>  
        /// <param name="result">The result to match.</param>  
        /// <param name="onSuccess">The action to execute if the result is successful.</param>  
        /// <param name="onFailure">The action to execute if the result is a failure.</param>  
        /// <returns>The original result.</returns>  
        public static Result Match(this Result result, Action onSuccess, Action onFailure)
        {
            switch (result.IsSuccess)
            {
                case true: onSuccess(); break;
                case false: onFailure(); break;
            }

            return result;
        }

        /// <summary>  
        /// Matches the result and executes the corresponding action based on success or failure.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The result to match.</param>  
        /// <param name="onSuccess">The action to execute if the result is successful.</param>  
        /// <param name="onFailure">The action to execute if the result is a failure.</param>  
        /// <returns>The original result.</returns>  
        public static Result<TResult> Match<TResult>(this Result<TResult> result, Action<TResult> onSuccess, Action<ReadOnlyCollection<Error>> onFailure)
        {
            switch (result.IsSuccess)
            {
                case true: onSuccess(result); break;
                case false: onFailure(result); break;
            }

            return result;
        }
    }
}
