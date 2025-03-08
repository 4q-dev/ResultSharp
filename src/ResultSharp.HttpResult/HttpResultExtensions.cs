using ResultSharp.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ResultSharp.Errors.Enums;
using ResultSharp.Errors;

namespace ResultSharp.HttpResult;

/// <summary>
/// Extension methods for <see cref="Result"/> to convert to <see cref="IActionResult"/>.
/// </summary>
public static class HttpResultExtensions
{
    /// <summary>
    /// Converts the <see cref="Result"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static ActionResult ToResponse(this Result result)
    {
        return result.IsSuccess
         ? new OkResult()
         : CreateErrorResponse(result.Errors);
    }

    /// <summary>
    /// Converts the <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="result">The async result to convert</param>
    /// <param name="configureAwait">Indicates whether to configure await.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static async Task<ActionResult> ToResponseAsync(this Task<Result> result, bool configureAwait = true)
        => ToResponse(await result.ConfigureAwait(configureAwait));

    /// <summary>
    /// Creates an error response from the specified errors.
    /// </summary>
    /// <param name="errors">The errors to include in the response.</param>
    /// <returns>An <see cref="ObjectResult"/> representing the error response.</returns>
    private static ObjectResult CreateErrorResponse(IEnumerable<Error> errors)
    {
        var errorCode = errors.First().ErrorCode;
        var isStatusCode = Enum.IsDefined(typeof(HttpStatusCode), (int)errorCode);

        if (isStatusCode)
            return new ObjectResult(errors) { StatusCode = (int)errorCode };

        return new ObjectResult(errors)
        {
            StatusCode = TranslateCustomErrorCodeToStatusCode(errorCode)
        };
    }

    /// <summary>
    /// Translates a custom error code to an HTTP status code.
    /// </summary>
    /// <param name="errorCode">The custom error code to translate.</param>
    /// <returns>The corresponding HTTP status code.</returns>
    private static int TranslateCustomErrorCodeToStatusCode(ErrorCode errorCode)
    {
        return errorCode switch
        {
            ErrorCode.Validation => (int)ErrorCode.BadRequest,
            _ => (int)ErrorCode.InternalServerError
        };
    }

    /// <summary>
    /// Converts the <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <returns>An <see cref="ActionResult{T}"/> representing the result.</returns>
    public static IActionResult ToResponse<T>(this Result<T> result)
    {
        return result.IsSuccess
         ? new OkObjectResult(result.Value)
         : CreateErrorResponse(result.Errors);
    }

    /// <summary>
    /// Converts the <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <param name="configureAwait">Indicates whether to configure await.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static async Task<IActionResult> ToResponseAsync<T>(this Task<Result<T>> result, bool configureAwait = true)
        => ToResponse(await result.ConfigureAwait(configureAwait));
}
