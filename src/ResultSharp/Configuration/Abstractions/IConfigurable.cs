namespace ResultSharp.Configuration.Abstractions
{
    public interface IConfigurable<TOptions>
    {
        public void Configure(Action<TOptions> configure);
    }
}
