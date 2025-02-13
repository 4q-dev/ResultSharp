using ResultSharp.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ResultSharp.Configuration.Logging
{
    /// <summary>
    /// Represents the options for configuring logging.
    /// </summary>
    public sealed record LoggingConfigurationOptions
    {
        private ILoggingAdapter? loggingAdapter;

        /// <summary>
        /// Gets or sets the logging adapter.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when try get the logging adapter when is not set.</exception>
        /// <exception cref="ArgumentNullException">Thrown when attempting to set a null logging adapter.</exception>
        /// <exception cref="InvalidOperationException">Thrown when attempting to set the logging adapter more than once.</exception>
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

        /// <summary>
        /// Determines whether the configuration options are invalid.
        /// </summary>
        /// <param name="errorMessage">The error message if the configuration is invalid.</param>
        /// <returns><c>true</c> if the configuration is invalid; otherwise, <c>false</c>.</returns>
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
