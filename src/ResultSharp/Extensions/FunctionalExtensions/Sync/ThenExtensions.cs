namespace ResultSharp.Extensions.FunctionalExtensions.Sync
{
    public static class ThenExtensions
    {
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
    }
}
