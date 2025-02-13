namespace ResultSharp.Configuration.Abstractions
{
    /// <summary>
    /// Defines a method to configure options of type <typeparamref name="TOptions"/>.
    /// </summary>
    /// <typeparam name="TOptions">The type of the configuration options.</typeparam>
    public interface IConfigurable<TOptions>
    {
        /// <summary>
        /// Configures the specified options.
        /// </summary>
        /// <param name="configure">An action to configure the options.</param>
        public void Configure(Action<TOptions> configure);
    }
}
