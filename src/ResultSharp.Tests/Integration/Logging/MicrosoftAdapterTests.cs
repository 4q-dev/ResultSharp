using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ResultSharp.Configuration;
using ResultSharp.Errors;
using ResultSharp.Extensions.FunctionalExtensions.Sync;
using ResultSharp.Logging;
using ResultSharp.Logging.MicrosoftLogger;
using ResultSharp.Tests.Helpers;

namespace ResultSharp.Tests.Integration.Logging
{
    [TestFixture]
    internal class MicrosoftAdapterTests
    {
        [SetUp]
        public void SetUp()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => { builder.AddConsole(); builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); });
            ILogger logger = factory.CreateLogger("Кумыс?");

            new ResultConfigurationGlobal().Configure(options =>
            {
                options.LoggingConfiguration.Configure(logConfig =>
                    logConfig.LoggingAdapter = new MicrosoftLoggingAdapter(logger)
                );
            });
        }

        [TearDown]
        public void ResetConfiguration()
        {
            ConfigurationHelpers.ResetGloabalConfiguration();
        }

        [Test]
        public void Test()
        {
            Result.Success().LogInformation("Test message {pattern} - {lol}", "ResultLogger", 0, 1212.11);
            Result.Failure().LogTrace("Operation executed. {arg1}, {arg2}", "message context", 11, false);

            Result.Success().LogIfFailure("бим бим бам бам");

            Result<int>.Success(10).LogIfSuccess("value {0}");
            Result<int>.Success(10).LogIfSuccess("value {0}", "context", ResultSharp.Logging.LogLevel.Information, 100);
            Result<int>.Success(10).LogIfSuccess("value");

            Result.Failure(Error.Failure("some failure message"), Error.NotFound("not found message"))
                .LogIfFailure();

            Result.Failure(Error.Failure("some failure message"))
                .LogIfFailure();

            Result<int>.Success(-1)
                .Ensure(v => v > 0, Error.Failure("Что вершит судьбу человечества в этом мире? Некое незримое существо или закон, подобно Длани Господней парящей над миром? По крайне мере истинно то, что человек не властен даже над своей волей."))
                .Map(v => v + 10000)
                .LogIfFailure();

            Result<int>.Success(10)
                .LogIfSuccess(pattern: "start: {0}")
                .Map(x => x + 5)
                .LogIfSuccess(pattern: "midle: {0}")
                .Then(x => x > 10 ? Result<string>.Success("x > 10") : Result<string>.Success("x <= 10"))
                .LogIfSuccess("result is: {result}");

        }
    }
}
