using ResultSharp.Configuration.Logging;
using System.Diagnostics.CodeAnalysis;

namespace ResultSharp.Configuration
{
    public record ResultConfigurationOptions
    {
        public bool EnableLogging { get; set; } = true;
        public LoggingConfiguration LoggingConfiguration { get; } = new();

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
