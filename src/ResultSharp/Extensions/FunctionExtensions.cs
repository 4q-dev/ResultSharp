using ResultSharp.Errors;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ResultSharp.Extensions
{
    /// <summary>  
    /// Provides extension methods for handling and transforming Result objects.  
    /// </summary>  
    public static class FunctionExtensions
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

        /// <summary>  
        /// Executes the next action if the result is successful, otherwise returns a failed result.  
        /// </summary>  
        /// <typeparam name="TOld">The type of the original result value.</typeparam>  
        /// <typeparam name="TNew">The type of the new result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="nextAction">The next action to execute if the result is successful.</param>  
        /// <returns>The result of the next action or a failed result.</returns>  
        public static Result<TNew> Then<TOld, TNew>(this Result<TOld> result, Func<TOld, Result<TNew>> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(result),
                false => Result<TNew>.Failure(result.Errors)
            };

        /// <summary>  
        /// Executes the next action if the result is successful, otherwise returns a failed result.  
        /// </summary>  
        /// <typeparam name="TOld">The type of the original result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="nextAction">The next action to execute if the result is successful.</param>  
        /// <returns>The result of the next action or a failed result.</returns>  
        public static Result Then<TOld>(this Result<TOld> result, Func<TOld, Result> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(result),
                false => result.Errors.ToArray()
            };

        /// <summary>  
        /// Executes the next action if the result is successful, otherwise returns the original result.  
        /// </summary>  
        /// <param name="result">The original result.</param>  
        /// <param name="nextAction">The next action to execute if the result is successful.</param>  
        /// <returns>The result of the next action or the original result.</returns>  
        public static Result Then(this Result result, Func<Result> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(),
                false => result
            };

        /// <summary>  
        /// Executes the next action if the result is successful, otherwise returns a failed result.  
        /// </summary>  
        /// <typeparam name="TNew">The type of the new result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="nextAction">The next action to execute if the result is successful.</param>  
        /// <returns>The result of the next action or a failed result.</returns>  
        public static Result<TNew> Then<TNew>(this Result result, Func<Result<TNew>> nextAction)
            => result.IsSuccess switch
            {
                true => nextAction(),
                false => Result<TNew>.Failure(result.Errors)
            };

        /// <summary>  
        /// Maps the result value to a new value if the result is successful, otherwise returns a failed result.  
        /// </summary>  
        /// <typeparam name="TOld">The type of the original result value.</typeparam>  
        /// <typeparam name="TNew">The type of the new result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="map">The mapping function to apply if the result is successful.</param>  
        /// <returns>The mapped result or a failed result.</returns>  
        public static Result<TNew> Map<TOld, TNew>(this Result<TOld> result, Func<TOld, TNew> map)
            => result.IsSuccess switch
            {
                true => map(result),
                false => Result<TNew>.Failure(result.Errors)
            };

        /// <summary>  
        /// Ensures the result is successful, otherwise returns a specified error.  
        /// </summary>  
        /// <param name="result">The original result.</param>  
        /// <param name="onFailure">The error to return if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the specified error.</returns>  
        public static Result Ensure(this Result result, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => result,
                false => onFailure ?? Error.Failure()
            };

        /// <summary>  
        /// Ensures the result is successful and satisfies a predicate, otherwise returns a specified error.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="predicate">The predicate to satisfy if the result is successful.</param>  
        /// <param name="onFailure">The error to return if the result is a failure or does not satisfy the predicate.</param>  
        /// <returns>The original result if successful and satisfies the predicate, otherwise the specified error.</returns>  
        public static Result<TResult> Ensure<TResult>(this Result<TResult> result, Predicate<TResult> predicate, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => predicate(result) ? result : onFailure ?? Error.Failure(),
                false => result
            };

        /// <summary>  
        /// Ensures the result is successful, otherwise returns a specified error.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="onFailure">The error to return if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the specified error.</returns>  
        public static Result<TResult> Ensure<TResult>(this Result<TResult> result, Error? onFailure = default)
            => result.IsSuccess switch
            {
                true => result,
                false => onFailure ?? Error.Failure()
            };

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

        /// <summary>  
        /// Unwraps the result value if successful, otherwise returns a default value.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="defualt">The default value to return if the result is a failure.</param>  
        /// <returns>The result value if successful, otherwise the default value.</returns>  
        public static TResult UnwrapOrDefault<TResult>(this Result<TResult> result, TResult defualt)
        {
            if (result.IsSuccess)
                return result;

            return defualt;
        }

        /// <summary>  
        /// Unwraps the result value if successful, otherwise throws an exception.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <returns>The result value if successful.</returns>  
        /// <exception cref="InvalidOperationException">Thrown if the result is a failure.</exception>  
        public static TResult Unwrap<TResult>(this Result<TResult> result)
        {
            if (result.IsSuccess)
                return result;

            throw new InvalidOperationException(result.SummaryErrorMessages());
        }

        /// <summary>  
        /// Returns the original result if successful, otherwise executes an alternative function.  
        /// </summary>  
        /// <param name="result">The original result.</param>  
        /// <param name="alternative">The alternative function to execute if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the result of the alternative function.</returns>  
        public static Result OrElse(this Result result, Func<Result> alternative)
            => result.IsSuccess switch
            {
                true => result,
                false => alternative()
            };

        /// <summary>  
        /// Returns the original result if successful, otherwise executes an alternative function.  
        /// </summary>  
        /// <typeparam name="TResult">The type of the result value.</typeparam>  
        /// <param name="result">The original result.</param>  
        /// <param name="alternative">The alternative function to execute if the result is a failure.</param>  
        /// <returns>The original result if successful, otherwise the result of the alternative function.</returns>  
        public static Result<TResult> OrElse<TResult>(this Result<TResult> result, Func<Result<TResult>> alternative)
            => result.IsSuccess switch
            {
                true => result,
                false => alternative()
            };
    }
}
