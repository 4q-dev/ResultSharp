using ResultSharp.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ResultSharp.Configuration.Logging
{
    public sealed record LoggingConfigurationOptions
    {
        private ILoggingAdapter? loggingAdapter;

        public ILoggingAdapter LoggingAdapter
        {
            get
            {
                if (loggingAdapter is null)
                    throw new InvalidOperationException("LoggingAdapter is not set.");
                return loggingAdapter;
            }
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value), "LoggingAdapter cannot be null.");

                if (loggingAdapter is not null)
                    throw new InvalidOperationException("LoggingAdapter has already been set.");

                loggingAdapter = value;
            }
        }

        internal bool IsInvalid([MaybeNullWhen(false)] out string errorMessage)
        {
            if (loggingAdapter is null)
            {
                errorMessage = "LoggingAdapter must be set in the configuration.";
                return true;
            }

            errorMessage = default;
            return false;
        }
    }
}
