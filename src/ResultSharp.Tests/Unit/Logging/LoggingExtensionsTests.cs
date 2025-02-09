using Moq;
using NUnit.Framework;
using ResultSharp.Configuration;
using ResultSharp.Configuration.Abstractions;
using ResultSharp.Configuration.Logging;
using ResultSharp.Errors;
using ResultSharp.Logging;
using ResultSharp.Logging.Abstractions;
using ResultSharp.Tests.Unit.Configuratoin;
using System.Reflection;

namespace ResultSharp.Tests.Unit.Logging
{
    [TestFixture]
    public class LoggingExtensionsTests
    {

        #region Setup / TearDown

        private Mock<ILoggingAdapter> mockLogger;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILoggingAdapter>();
            new ResultConfigurationGlobal().Configure((options) =>
            {
                options.LoggingConfiguration.Configure((logConfig) => logConfig.LoggingAdapter = mockLogger.Object);
            });

            typeof(LoggingExtensions)
                .GetField("logger", BindingFlags.NonPublic | BindingFlags.Static)!
                .SetValue(null, mockLogger.Object);
        }

        [TearDown]
        public void Reset()
        {
            mockLogger.Reset();
            Helpers.ResetConfiguration(typeof(ResultConfigurationGlobal));
            Helpers.ResetConfiguration(typeof(ConfiguratoinBase<LoggingConfigurationOptions>));
        }

        #endregion

        #region Result logging

        [Test]
        public void LogTrace_ShouldCallLogWithTraceLevel()
        {
            var result = Result.Success();
            result.LogTrace("Trace message");

            mockLogger.Verify(logger => logger.Log("Trace message", LogLevel.Trace, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogDebug_ShouldCallLogWithDebugLevel()
        {
            var result = Result.Success();
            result.LogDebug("Debug message");

            mockLogger.Verify(logger => logger.Log("Debug message", LogLevel.Debug, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogInformation_ShouldCallLogWithInformationLevel()
        {
            var result = Result.Success();
            result.LogInformation("Information message");

            mockLogger.Verify(logger => logger.Log("Information message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogWarning_ShouldCallLogWithWarningLevel()
        {
            var result = Result.Success();
            result.LogWarning("Warning message");

            mockLogger.Verify(logger => logger.Log("Warning message", LogLevel.Warning, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogError_ShouldCallLogWithErrorLevel()
        {
            var result = Result.Success();
            result.LogError("Error message");

            mockLogger.Verify(logger => logger.Log("Error message", LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogCritical_ShouldCallLogWithCriticalLevel()
        {
            var result = Result.Success();
            result.LogCritical("Critical message");

            mockLogger.Verify(logger => logger.Log("Critical message", LogLevel.Critical, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogIfSuccess_ShouldLogWhenResultIsSuccess()
        {
            var result = Result.Success();
            result.LogIfSuccess("Success message");

            mockLogger.Verify(logger => logger.Log("Success message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogIfSuccess_ShouldNotLogWhenResultIsFailure()
        {
            var result = Result.Failure(new Error("Failure"));
            result.LogIfSuccess("Success message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        [Test]
        public void LogIfFailure_ShouldLogWhenResultIsFailure()
        {
            var result = Result.Failure(new Error("Failure"));
            result.LogIfFailure();

            mockLogger.Verify(logger => logger.Log(result.SummaryErrorMessages(), LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogIfFailure_ShouldNotLogWhenResultIsSuccess()
        {
            var result = Result.Success();
            result.LogIfFailure("Failure message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        #endregion

        #region Generic result logging

        [Test]
        public void LogTrace_ShouldCallLogWithTraceLevel_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogTrace("Trace message");

            mockLogger.Verify(logger => logger.Log("Trace message", LogLevel.Trace, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogDebug_ShouldCallLogWithDebugLevel_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogDebug("Debug message");

            mockLogger.Verify(logger => logger.Log("Debug message", LogLevel.Debug, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogInformation_ShouldCallLogWithInformationLevel_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogInformation("Information message");

            mockLogger.Verify(logger => logger.Log("Information message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogWarning_ShouldCallLogWithWarningLevel_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogWarning("Warning message");

            mockLogger.Verify(logger => logger.Log("Warning message", LogLevel.Warning, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogError_ShouldCallLogWithErrorLevel_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogError("Error message");

            mockLogger.Verify(logger => logger.Log("Error message", LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogCritical_ShouldCallLogWithCriticalLevel_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogCritical("Critical message");

            mockLogger.Verify(logger => logger.Log("Critical message", LogLevel.Critical, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogIfSuccess_ShouldLogWhenResultIsSuccess_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogIfSuccess("Success message");

            mockLogger.Verify(logger => logger.Log("Success message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogIfSuccess_ShouldNotLogWhenResultIsFailure_Generic()
        {
            var result = Result<int>.Failure(new Error("Failure"));
            result.LogIfSuccess("Success message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        [Test]
        public void LogIfFailure_ShouldLogWhenResultIsFailure_Generic()
        {
            var result = Result<int>.Failure(new Error("Failure"));
            result.LogIfFailure();

            mockLogger.Verify(logger => logger.Log(result.SummaryErrorMessages(), LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public void LogIfFailure_ShouldNotLogWhenResultIsSuccess_Generic()
        {
            var result = Result<int>.Success(1);
            result.LogIfFailure("Failure message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        #endregion
    }
}
