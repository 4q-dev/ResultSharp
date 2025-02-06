namespace ResultSharp.Astractions
{
    public interface IResult<TError> 
        where TError : IError
    {
        public bool IsSuccess { get; }
        public bool IsFailure { get; }
        public IReadOnlyCollection<TError> Errors { get; }
    }
}
