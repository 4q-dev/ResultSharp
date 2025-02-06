using System.Text;

namespace ResultSharp.Astractions;

public abstract class ResultBase<TError> :
    IResult<TError>
    where TError : IError
{
    private readonly TError[] errors;

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyCollection<TError> Errors => IsFailure ? errors!.AsReadOnly() : throw new InvalidOperationException($"Error can not be accessed when {nameof(IsFailure)} is false");

    protected ResultBase(bool isSuccess, TError[]? errors)
    {
        IsSuccess = isSuccess;
        this.errors = errors ?? Array.Empty<TError>();
    }

    public string SummaryErrorMessages()
    {
        var sb = new StringBuilder();

        foreach (var error in Errors)
            sb.AppendLine(error.Message);

        return sb.ToString();
    }
}