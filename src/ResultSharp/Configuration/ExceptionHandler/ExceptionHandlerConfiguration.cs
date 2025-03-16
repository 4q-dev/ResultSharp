using ResultSharp.Configuration.Abstractions;

namespace ResultSharp.Configuration.ExceptionHandler
{
    /// <summary>
    /// Configuration for the exception handler.
    /// </summary>
    public class ExceptionHandlerConfiguration :
        ConfigurationBase<ExceptionHandlerOptions>,
        IConfigurable<ExceptionHandlerOptions>
    {
        private readonly object locker = new();

        /// <summary>
        /// Configures the exception handler.
        /// </summary>
        /// <param name="configure">The action to configure the exception handler.</param>
        /// <exception cref="InvalidOperationException">Thrown when the configuration has already been set.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the configure action is null.</exception>
        public void Configure(Action<ExceptionHandlerOptions> configure)
        {
            lock (locker)
            {
                ArgumentNullException.ThrowIfNull(configure); 

                var newOptions = new ExceptionHandlerOptions();
                configure(newOptions); 

                if (IsConfigured)
                    throw new InvalidOperationException("ExceptionHandlerConfiguration configuration has already been set.");

                ApplyConfiguration(newOptions); 
            }
        }
    }
}
