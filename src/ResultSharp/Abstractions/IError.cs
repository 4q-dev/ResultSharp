namespace ResultSharp.Abstractions
{
    /// <summary>  
    /// Represents an error with a message.  
    /// </summary>  
    public interface IError
    {
        /// <summary>  
        /// Gets the error message.  
        /// </summary>  
        public string Message { get; }
    }
}
