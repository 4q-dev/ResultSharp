using ResultSharp.Configuration.Abstractions;
using ResultSharp.Logging.Abstractions;

namespace ResultSharp.Configuration.Logging
{
    public sealed class LoggingConfiguration :
        ConfiguratoinBase<LoggingConfigurationOptions>,
        IConfigurable<LoggingConfigurationOptions>
    {
        private readonly object locker = new();      

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

        internal ILoggingAdapter GetLogger()
            => Options.LoggingAdapter;
    }
}
