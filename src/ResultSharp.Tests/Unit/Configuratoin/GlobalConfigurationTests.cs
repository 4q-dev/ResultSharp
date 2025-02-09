using Moq;
using NUnit.Framework;
using ResultSharp.Configuration;
using ResultSharp.Logging.Abstractions;
using ResultSharp.Tests.Helpers;

namespace ResultSharp.Tests.Unit.Configuratoin
{
    internal class GlobalConfigurationTests
    {
        [TearDown]
        public void ResetStaticMembers()
        {
            ConfigurationHelpers.ResetGloabalConfiguration();
        }

        [Test]
        public void IsInvalid_ShouldReturnFalse_WhenLoggingDontUse()
        {
            var options = new ResultConfigurationOptions
            {
                EnableLogging = false
            };

            var result = options.IsInvalid(out var errorMessage);

            Assert.IsFalse(result);
            Assert.IsNull(errorMessage);
        }

        [Test]
        public void IsInvalid_ShouldReturnFalse_WhenConfigurationIsValid()
        {
            var options = new ResultConfigurationOptions();
            options.LoggingConfiguration.Configure((options) =>
            {
                options.LoggingAdapter = new Mock<ILoggingAdapter>().Object;
            });

            var result = options.IsInvalid(out var errorMessage);

            Assert.IsFalse(result);
            Assert.IsNull(errorMessage);
        }

        [Test]
        public void IsInvalid_ShouldReturnTrue_WhenLoggingIsEnabledButLoggingConfigurationIsNotSetUp()
        {
            var options = new ResultConfigurationOptions
            {
                EnableLogging = true
            };

            var result = options.IsInvalid(out var errorMessage);

            Assert.IsTrue(result);
            Assert.AreEqual("LoggingConfiguration configuration must be set up when logging is enabled.", errorMessage);
        }

        [Test]
        public void Configure_ShouldSetOptions_WhenCalledOnce()
        {
            var config = new ResultConfigurationGlobal();
            var mockLogger = new Mock<ILoggingAdapter>();

            config.Configure(options =>
            {
                options.EnableLogging = true;
                options.LoggingConfiguration.Configure(loggingOptions => loggingOptions.LoggingAdapter = mockLogger.Object);
            });

            Assert.IsTrue(ResultConfigurationGlobal.IsConfigured);
            Assert.IsTrue(ResultConfigurationGlobal.GlobalOptions.EnableLogging);
            Assert.AreEqual(mockLogger.Object, ResultConfigurationGlobal.GlobalOptions.LoggingConfiguration.GetLogger());
        }

        [Test]
        public void Configure_ShouldThrowException_WhenCalledTwice()
        {
            var config = new ResultConfigurationGlobal();
            var mockLogger = new Mock<ILoggingAdapter>();

            config.Configure(options =>
            {
                options.EnableLogging = true;
                options.LoggingConfiguration.Configure(loggingOptions => loggingOptions.LoggingAdapter = mockLogger.Object);
            });

            Assert.Throws<InvalidOperationException>(() =>
                config.Configure(options =>
                {
                    options.EnableLogging = true;
                    options.LoggingConfiguration.Configure(loggingOptions => loggingOptions.LoggingAdapter = mockLogger.Object);
                })
            );
        }

        [Test]
        public void GetLogger_ShouldReturnLogger_WhenLoggingIsEnabled()
        {
            var config = new ResultConfigurationGlobal();
            var mockLogger = new Mock<ILoggingAdapter>();

            config.Configure(options =>
            {
                options.EnableLogging = true;
                options.LoggingConfiguration.Configure(loggingOptions => loggingOptions.LoggingAdapter = mockLogger.Object);
            });

            var logger = ResultConfigurationGlobal.GetLogger();

            Assert.AreEqual(mockLogger.Object, logger);
        }

        [Test]
        public void GetLogger_ShouldThrowException_WhenLoggingIsDisabled()
        {
            var config = new ResultConfigurationGlobal();

            config.Configure(options => options.EnableLogging = false);

            Assert.Throws<InvalidOperationException>(() => ResultConfigurationGlobal.GetLogger());
        }
    }
}
