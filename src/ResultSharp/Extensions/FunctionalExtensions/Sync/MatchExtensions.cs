using ResultSharp.Core;
using ResultSharp.Errors;
using System.Collections.ObjectModel;

namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    /// <summary>
    /// Provides extension methods for pattern matching on <see cref="Result"/> types.
    /// These methods allow for handling both success and failure cases in a functional way,
    /// executing different actions based on the result state.
    /// </summary>
    public static class MatchExtensions
    {
        /// <summary>
        /// Matches a <see cref="Result"/> and executes different actions based on success or failure.
        /// </summary>
        /// <param name="result">The <see cref="Result"/> to match against</param>
        /// <param name="onSuccess">Action to execute when the result is successful</param>
        /// <param name="onFailure">Action to execute when the result has failed</param>
        /// <returns>A new <see cref="Result"/> based on the executed action</returns>
        public static Result Match(this Result result, Func<Result> onSuccess, Func<ReadOnlyCollection<Error>, Result> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(),
                false => onFailure(result)
            };
        }

        /// <summary>
        /// Matches a <see cref="Result"/> of one type and executes different actions based on success or failure,
        /// transforming the result to a non-generic <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TOld">The type of the original <see cref="Result{TOld}"/></typeparam>
        /// <param name="result">The <see cref="Result{TOld}"/> to match against</param>
        /// <param name="onSuccess">Action to execute when the result is successful</param>
        /// <param name="onFailure">Action to execute when the result has failed</param>
        /// <returns>A new non-generic <see cref="Result"/> based on the executed action</returns>
        public static Result Match<TOld>(this Result<TOld> result, Func<TOld, Result> onSuccess, Func<ReadOnlyCollection<Error>, Result> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(result),
                false => onFailure(result)
            };
        }

        /// <summary>
        /// Matches a <see cref="Result"/> and executes different actions based on success or failure,
        /// transforming the result to a new type.
        /// </summary>
        /// <typeparam name="TNew">The type of the new <see cref="Result{TNew}"/></typeparam>
        /// <param name="result">The <see cref="Result"/> to match against</param>
        /// <param name="onSuccess">Action to execute when the result is successful</param>
        /// <param name="onFailure">Action to execute when the result has failed</param>
        /// <returns>A new <see cref="Result{TNew}"/> based on the executed action</returns>
        public static Result<TNew> Match<TNew>(this Result result, Func<Result<TNew>> onSuccess, Func<ReadOnlyCollection<Error>, Result<TNew>> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(),
                false => onFailure(result)
            };
        }

        /// <summary>
        /// Matches a <see cref="Result{TOld}"/> and executes different actions based on success or failure,
        /// transforming the result to a new type.
        /// </summary>
        /// <typeparam name="TOld">The type of the original <see cref="Result{TOld}"/></typeparam>
        /// <typeparam name="TNew">The type of the new <see cref="Result{TNew}"/></typeparam>
        /// <param name="result">The <see cref="Result{TOld}"/> to match against</param>
        /// <param name="onSuccess">Action to execute when the result is successful</param>
        /// <param name="onFailure">Action to execute when the result has failed</param>
        /// <returns>A new <see cref="Result{TNew}"/> based on the executed action</returns>
        public static Result<TNew> Match<TOld, TNew>(this Result<TOld> result, Func<TOld, Result<TNew>> onSuccess, Func<ReadOnlyCollection<Error>, Result<TNew>> onFailure)
        {
            return result.IsSuccess switch
            {
                true => onSuccess(result),
                false => onFailure(result)
            };
        }
    }
}
