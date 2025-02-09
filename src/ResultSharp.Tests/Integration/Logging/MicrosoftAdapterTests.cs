using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ResultSharp.Configuration;
using ResultSharp.Errors;
using ResultSharp.Logging;
using ResultSharp.Logging.MicrosoftLogger;
using System.Xml.Schema;

namespace ResultSharp.Tests.Integration.Logging
{
    [TestFixture]
    internal class MicrosoftAdapterTests
    {
        [SetUp]
        public void SetUp()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Кумыс?");

            new ResultConfigurationGlobal().Configure(options =>
            {
                options.LoggingConfiguration.Configure(logConfig => 
                    logConfig.LoggingAdapter = new MicrosoftLoggingAdapter(logger)
                );
            });
        }

        [Test]
        public void Test()
        {
            Result.Success().LogInformation("Test message");

            Result.Success().LogIfFailure("бим бим бам бам");

            Result<int>.Success(10).LogIfSuccess("value {0}");

            Result.Failure(Error.Failure("some failure message"), Error.NotFound("not found message"))
                .LogIfFailure();
        }
    }
}
