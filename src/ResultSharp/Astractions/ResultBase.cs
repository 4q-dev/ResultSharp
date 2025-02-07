using System.Text;

namespace ResultSharp.Astractions;

/// <summary>
/// Represents the base class for result objects, containing success status and errors if any.
/// </summary>
/// <typeparam name="TError">The type of the error.</typeparam>
public abstract class ResultBase<TError> : IResult<TError>
    where TError : IError
{
    private readonly TError[] errors;

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the collection of errors associated with the operation.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when accessing errors if the operation was successful.</exception>
    public IReadOnlyCollection<TError> Errors => IsFailure ? errors!.AsReadOnly() : throw new InvalidOperationException($"Error can not be accessed when {nameof(IsFailure)} is false");

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultBase{TError}"/> class.
    /// </summary>
    /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
    /// <param name="errors">The errors associated with the operation.</param>
    protected ResultBase(bool isSuccess, TError[]? errors)
    {
        IsSuccess = isSuccess;
        this.errors = errors ?? Array.Empty<TError>();
    }

    /// <summary>
    /// Gets a summary of error messages.
    /// </summary>
    /// <returns>A string containing all error messages.</returns>
    public string SummaryErrorMessages()
    {
        var sb = new StringBuilder();

        foreach (var error in Errors)
            sb.AppendLine(error.Message);

        return sb.ToString();
    }
}
