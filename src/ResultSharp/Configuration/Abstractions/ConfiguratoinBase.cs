namespace ResultSharp.Configuration.Abstractions
{
    /// <summary>
    /// Provides a base class for configuration with options of type <typeparamref name="TOptions"/>.
    /// </summary>
    /// <typeparam name="TOptions">The type of the configuration options.</typeparam>
    public abstract class ConfiguratoinBase<TOptions>
    {
        /// <summary>
        /// Gets a value indicating whether the configuration has been applied.
        /// </summary>
        public bool IsConfigured { get; private set; } = false;

        /// <summary>
        /// Gets the configuration options.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the configuration has not been set.</exception>
        public TOptions Options => options ?? throw new InvalidOperationException($"Configuration for {typeof(TOptions).Name} has not been set.");

        private TOptions? options;

        /// <summary>
        /// Applies the specified configuration options.
        /// </summary>
        /// <param name="newOptions">The new configuration options to apply.</param>
        internal protected void ApplyConfiguration(TOptions newOptions)
        {
            IsConfigured = true;
            options = newOptions;
        }
    }
}
