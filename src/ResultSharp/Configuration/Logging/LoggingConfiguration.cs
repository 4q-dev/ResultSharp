using ResultSharp.Configuration.Abstractions;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Configuration.Logging
{
    /// <summary>  
    /// Provides configuration for logging.  
    /// </summary>  
    public sealed class LoggingConfiguration :
        ConfiguratoinBase<LoggingConfigurationOptions>,
        IConfigurable<LoggingConfigurationOptions>
    {
        private readonly object locker = new();

        /// <summary>  
        /// Configures the logging options.  
        /// </summary>  
        /// <param name="configure">An action to configure the logging options.</param>  
        /// <exception cref="InvalidOperationException">Thrown when the configuration has already been set.</exception>  
        /// <exception cref="ArgumentException">Thrown when the configuration options are invalid.</exception>  
        public void Configure(Action<LoggingConfigurationOptions> configure)
        {
            lock (locker)
            {
                if (IsConfigured)
                    throw new InvalidOperationException("LoggingConfiguration configuration has already been set.");

                var newOptions = new LoggingConfigurationOptions();
                configure(newOptions);

                if (newOptions.IsInvalid(out var errorMessage))
                    throw new ArgumentException(errorMessage);

                ApplyConfiguration(newOptions);
            }
        }

        /// <summary>  
        /// Gets the logger from the configured options.  
        /// </summary>  
        /// <returns>The configured logger.</returns>  
        internal ILoggingAdapter GetLogger()
            => Options.LoggingAdapter;
    }
}
