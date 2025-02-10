using ResultSharp.Configuration.Abstractions;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Configuration
{
    public sealed class ResultConfigurationGlobal : IConfigurable<ResultConfigurationOptions>
    {
        public static bool IsConfigured { get; private set; } = false;
        public static ResultConfigurationOptions GlobalOptions => options ?? throw new InvalidOperationException($"Global configuration has not been set.");
        
        private static ResultConfigurationOptions? options;
        private static readonly object locker = new();

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

        private void ApplyConfiguration(ResultConfigurationOptions newOptions)
        {
            IsConfigured = true;
            options = newOptions;
        }

        internal static ILoggingAdapter GetLogger()
        {
            if (!GlobalOptions.EnableLogging)
                throw new InvalidOperationException("LoggingConfiguration is disabled");

            return GlobalOptions.LoggingConfiguration.GetLogger();
        }
    }
}
