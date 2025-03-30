using ResultSharp.Core;
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
        /// Executes the appropriate function based on the result's state and returns a new Result.
        /// </summary>
        /// <param name="result">The result to match against.</param>
        /// <param name="onSuccess">Function to execute when the result is successful.</param>
        /// <param name="onFailure">Function to execute when the result is a failure, providing access to the error collection.</param>
        /// <returns>A new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static Result Match(this Result result, Func<Result> onSuccess, Func<ReadOnlyCollection<Error>, Result> onFailure)
        {
            switch (result.IsSuccess)
            {
                case true: return onSuccess();
                case false: return onFailure(result);
            }
        }

        /// <summary>
        /// Executes the appropriate function based on the result's state and returns a new Result with a different type.
        /// </summary>
        /// <typeparam name="TNew">The type of the value in the new Result.</typeparam>
        /// <param name="result">The result to match against.</param>
        /// <param name="onSuccess">Function to execute when the result is successful.</param>
        /// <param name="onFailure">Function to execute when the result is a failure, providing access to the error collection.</param>
        /// <returns>A new <see cref="Result{TNew}"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static Result<TNew> Match<TNew>(this Result result, Func<Result<TNew>> onSuccess, Func<ReadOnlyCollection<Error>, Result<TNew>> onFailure)
        {
            switch (result.IsSuccess)
            {
                case true: return onSuccess();
                case false: return onFailure(result);
            }
        }

        /// <summary>
        /// Executes the appropriate function based on the result's state and returns a new Result with a different type.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <typeparam name="TNew">The type of the value in the new Result.</typeparam>
        /// <param name="result">The result to match against.</param>
        /// <param name="onSuccess">Function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Function to execute when the result is a failure, providing access to the error collection.</param>
        /// <returns>A new <see cref="Result{TNew}"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static Result<TNew> Match<TResult, TNew>(this Result<TResult> result, Func<TResult, Result<TNew>> onSuccess, Func<ReadOnlyCollection<Error>, Result<TNew>> onFailure)
        {
            switch (result.IsSuccess)
            {
                case true: return onSuccess(result);
                case false: return onFailure(result);
            }
        }

        /// <summary>
        /// Executes the appropriate function based on the result's state and transforms a typed Result to a non-typed Result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the original Result.</typeparam>
        /// <param name="result">The result to match against.</param>
        /// <param name="onSuccess">Function to execute when the result is successful, providing access to the result value.</param>
        /// <param name="onFailure">Function to execute when the result is a failure, providing access to the error collection.</param>
        /// <returns>A new <see cref="Result"/> instance returned by either the onSuccess or onFailure function.</returns>
        public static Result Match<TResult>(this Result<TResult> result, Func<TResult, Result> onSuccess, Func<ReadOnlyCollection<Error>, Result> onFailure)
        {
            switch (result.IsSuccess)
            {
                case true: return onSuccess(result);
                case false: return onFailure(result);
            }
        }
    }
}
