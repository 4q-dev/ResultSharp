using ResultSharp.Configuration.Logging;
using System.Diagnostics.CodeAnalysis;

namespace ResultSharp.Configuration
{
    /// <summary>
    /// Represents the options for configuring result handling.
    /// </summary>
    public sealed record ResultConfigurationOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether logging is enabled.
        /// </summary>
        public bool EnableLogging { get; set; } = true;

        /// <summary>
        /// Gets the logging configuration.
        /// </summary>
        public LoggingConfiguration LoggingConfiguration { get; } = new();

        /// <summary>
        /// Determines whether the configuration options are invalid.
        /// </summary>
        /// <param name="errorMessage">The error message if the configuration is invalid.</param>
        /// <returns><c>true</c> if the configuration is invalid; otherwise, <c>false</c>.</returns>
        internal bool IsInvalid([MaybeNullWhen(false)] out string errorMessage)
        {
            if (EnableLogging && LoggingConfiguration.IsConfigured is false)
            {
                errorMessage = "LoggingConfiguration configuration must be set up when logging is enabled.";
                return true;
            }

            errorMessage = default;
            return false;
        }
    }
}
