using Moq;
using NUnit.Framework;
using ResultSharp.Configuration.Abstractions;
using ResultSharp.Configuration.Logging;
using ResultSharp.Logging.Abstractions;
using ResultSharp.Tests.Helpers;

namespace ResultSharp.Tests.Unit.Configuratoin
{
    [TestFixture]
    public class LoggingConfigurationOptionsTests
    {
        [SetUp]
        public void ResetStaticMembers()
        {
            ConfigurationHelpers.ResetConfiguration(typeof(ConfiguratoinBase<LoggingConfigurationOptions>));
        }

        [Test]
        public void Getter_ShouldThrowException_WhenLoggingAdapterIsNull()
        {
            var options = new LoggingConfigurationOptions();
            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = options.LoggingAdapter;
            });
        }

        [Test]
        public void Setter_ShouldThrowException_WhenValueIsNull()
        {
            var options = new LoggingConfigurationOptions();
            Assert.Throws<ArgumentNullException>(() =>
            {
                options.LoggingAdapter = null!;
            });
        }

        [Test]
        public void Setter_ShouldThrowException_WhenCallTwice()
        {
            var options = new LoggingConfigurationOptions();
            Assert.Throws<InvalidOperationException>(() =>
            {
                options.LoggingAdapter = new Mock<ILoggingAdapter>().Object;
                options.LoggingAdapter = new Mock<ILoggingAdapter>().Object;
            });
        }

        [Test]
        public void IsInvalid_ShouldReturnTrue_WhenLoggingAdapterIsNull()
        {
            var options = new LoggingConfigurationOptions();

            var result = options.IsInvalid(out var errorMessage);

            Assert.IsTrue(result);
            Assert.AreEqual("LoggingAdapter must be set in the configuration.", errorMessage);
        }

        [Test]
        public void IsInvalid_ShouldReturnFalse_WhenLoggingAdapterIsNotNull()
        {
            var mockLogger = new Mock<ILoggingAdapter>();
            var options = new LoggingConfigurationOptions
            {
                LoggingAdapter = mockLogger.Object
            };

            var result = options.IsInvalid(out var errorMessage);

            Assert.IsFalse(result);
            Assert.IsNull(errorMessage);
        }

        [Test]
        public void Configure_ShouldSetOptions_WhenCalledOnce()
        {
            var config = new LoggingConfiguration();
            var mockLogger = new Mock<ILoggingAdapter>();

            config.Configure(options => options.LoggingAdapter = mockLogger.Object);

            Assert.IsTrue(config.IsConfigured);
            Assert.AreEqual(mockLogger.Object, config.Options.LoggingAdapter);
        }

        [Test]
        public void Configure_ShouldThrowException_WhenCalledTwice()
        {
            var config = new LoggingConfiguration();
            var mockLogger = new Mock<ILoggingAdapter>();

            config.Configure(options => options.LoggingAdapter = mockLogger.Object);

            Assert.Throws<InvalidOperationException>(() =>
                config.Configure(options => options.LoggingAdapter = mockLogger.Object));
        }

        [Test]
        public void Configure_ShouldThrowException_WhenCalledTwiceWithDifferenctConfigurationObjects()
        {
            var mockLogger = new Mock<ILoggingAdapter>();
            var config1 = new LoggingConfiguration();
            config1.Configure((options) =>
            {
                options.LoggingAdapter = mockLogger.Object;
            });

            Assert.That(LoggingConfiguration.IsConfigured);
            Assert.AreEqual(config1.Options.LoggingAdapter, mockLogger.Object);

            var config2 = new LoggingConfiguration();
            Assert.Throws<InvalidOperationException>(() =>
                config2.Configure((options) =>
                {
                    options.LoggingAdapter = mockLogger.Object;
                })
            );
        }

        [Test]
        public void GetLogger_ShouldReturnLogger_WhenConfigured()
        {
            var config = new LoggingConfiguration();
            var mockLogger = new Mock<ILoggingAdapter>();

            config.Configure(options => options.LoggingAdapter = mockLogger.Object);
            var logger = config.GetLogger();

            Assert.AreEqual(mockLogger.Object, logger);
        }

        [Test]
        public void GetLogger_ShouldThrowException_WhenLoggingAdapterIsNull()
        {
            var config = new LoggingConfiguration();

            Assert.Throws<InvalidOperationException>(() => config.GetLogger());
        }
    }
}

