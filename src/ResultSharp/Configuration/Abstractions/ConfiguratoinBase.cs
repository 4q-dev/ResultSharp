namespace ResultSharp.Configuration.Abstractions
{
    public abstract class ConfiguratoinBase<TOptions>
    {
        public static bool IsConfigured { get; private set; } = false;
        public TOptions Options => options ?? throw new InvalidOperationException($"Configuration for {typeof(TOptions).Name} has not been set.");

        private static TOptions? options;

        protected void ApplyConfiguration(TOptions newOptions)
        {
            IsConfigured = true;
            options = newOptions;
        }
    }
}
