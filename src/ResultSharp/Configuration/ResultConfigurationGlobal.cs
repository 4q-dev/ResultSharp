using ResultSharp.Configuration.Abstractions;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Configuration
{
    /// <summary>  
    /// Provides global configuration for result handling.  
    /// </summary>  
    public sealed class ResultConfigurationGlobal : IConfigurable<ResultConfigurationOptions>
    {
        /// <summary>  
        /// Gets a value indicating whether the global configuration has been applied.  
        /// </summary>  
        public static bool IsConfigured { get; private set; } = false;

        /// <summary>  
        /// Gets the global configuration options.  
        /// </summary>  
        /// <exception cref="InvalidOperationException">Thrown when the global configuration has not been set.</exception>  
        public static ResultConfigurationOptions GlobalOptions => options ?? throw new InvalidOperationException($"Global configuration has not been set.");

        private static ResultConfigurationOptions? options;
        private static readonly object locker = new();

        /// <summary>  
        /// Configures the global result handling options.  
        /// </summary>  
        /// <param name="configure">An action to configure the result handling options.</param>  
        /// <exception cref="InvalidOperationException">Thrown when the configuration has already been set.</exception>  
        /// <exception cref="ArgumentException">Thrown when the configuration options are invalid.</exception>  
        public void Configure(Action<ResultConfigurationOptions> configure)
        {
            lock (locker)
            {
                if (IsConfigured)
                    throw new InvalidOperationException("Result configuration has already been set.");

                var newOptions = new ResultConfigurationOptions();
                configure(newOptions);

                if (newOptions.IsInvalid(out var errorMessage))
                    throw new ArgumentException(errorMessage);

                ApplyConfiguration(newOptions);
            }
        }

        /// <summary>  
        /// Applies the specified configuration options.  
        /// </summary>  
        /// <param name="newOptions">The new configuration options to apply.</param>  
        private void ApplyConfiguration(ResultConfigurationOptions newOptions)
        {
            IsConfigured = true;
            options = newOptions;
        }

        /// <summary>  
        /// Gets the logger from the global configuration options.  
        /// </summary>  
        /// <returns>The configured logger.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when logging is disabled.</exception>  
        internal static ILoggingAdapter GetLogger()
        {
            if (!GlobalOptions.EnableLogging)
                throw new InvalidOperationException("LoggingConfiguration is disabled");

            return GlobalOptions.LoggingConfiguration.GetLogger();
        }
    }
}
