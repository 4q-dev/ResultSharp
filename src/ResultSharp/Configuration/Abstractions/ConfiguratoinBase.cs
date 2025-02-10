namespace ResultSharp.Configuration.Abstractions
{
    public abstract class ConfiguratoinBase<TOptions>
    {
        public bool IsConfigured { get; private set; } = false;
        public TOptions Options => options ?? throw new InvalidOperationException($"Configuration for {typeof(TOptions).Name} has not been set.");

        private TOptions? options;

        protected void ApplyConfiguration(TOptions newOptions)
        {
            IsConfigured = true;
            options = newOptions;
        }
    }
}
