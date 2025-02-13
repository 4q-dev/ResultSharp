namespace ResultSharp.Logging
{
    /// <summary>
    /// Specifies the log level for logging messages.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Logs that contain the most detailed messages. These messages may contain sensitive application data.
        /// </summary>
        Trace,

        /// <summary>
        /// Logs that are used for interactive investigation during development. These logs should primarily contain information useful for debugging and have no long-term value.
        /// </summary>
        Debug,

        /// <summary>
        /// Logs that track the general flow of the application. These logs should have long-term value.
        /// </summary>
        Information,

        /// <summary>
        /// Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application to stop.
        /// </summary>
        Warning,

        /// <summary>
        /// Logs that highlight when the current flow of execution is stopped due to a failure. These should indicate a failure in the current activity, not an application-wide failure.
        /// </summary>
        Error,

        /// <summary>
        /// Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.
        /// </summary>
        Critical
    }
}
