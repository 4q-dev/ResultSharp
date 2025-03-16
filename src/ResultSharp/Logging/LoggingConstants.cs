namespace ResultSharp.Logging
{
    /// <summary>
    /// Contains constant string values used in logging extensions.
    /// </summary>
    public static class LoggingConstants
    {
        /// <summary>
        /// Default context for logging.
        /// </summary>
        public const string DefaultContext = "ResultLogger";

        /// <summary>
        /// Default message for successful operation logging.
        /// </summary>
        public const string DefaultSuccessMessage = "Operation success";

        /// <summary>
        /// Default message pattern for logging successful result values.
        /// </summary>
        public const string DefaultSuccessPattern = "{value}";
    }
}
