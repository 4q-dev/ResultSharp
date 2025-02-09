using NUnit.Framework;
using ResultSharp.Extensions.FunctionalExtensions.Sync;
using ResultSharp.Configuration;
using Serilog;
using ResultSharp.Errors;
using ResultSharp.Logging;
using ResultSharp.Logging.Serilog;
using ResultSharp.Tests.Helpers;

namespace ResultSharp.Tests.Integration.Logging
{
    [TestFixture]
    internal class SerilogAdapterTests
    {
        [SetUp]
        public void SetUp()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            new ResultConfigurationGlobal().Configure(options =>
            {
                options.LoggingConfiguration.Configure(logConfig =>
                    logConfig.LoggingAdapter = new SerilogAdapter(logger)
                );
            });
        }

        [TearDown]
        public void ResetConfiguratoin()
        {
            ConfigurationHelpers.ResetGloabalConfiguration();
        }

        [Test]
        public void Test()
        {
            Result.Success().LogInformation("Serilog message");

            Result.Success().LogIfFailure("бим бим бам бам");

            Result<int>.Success(10).LogIfSuccess("value {0}");

            Result.Failure(Error.Failure("some failure message"), Error.NotFound("not found message"))
                .LogIfFailure();

            Result.Failure(Error.Failure("some failure message"))
                .LogIfFailure();

            Result<int>.Success(-1)
                .Ensure(v => v > 0, Error.Failure("Что вершит судьбу человечества в этом мире? Некое незримое существо или закон, подобно Длани Господней парящей над миром? По крайне мере истинно то, что человек не властен даже над своей волей."))
                .Map(v => v + 10000)
                .LogIfFailure();
        }
    }
}
