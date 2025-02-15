using Moq;
using NUnit.Framework;
using ResultSharp.Configuration;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Logging;
using ResultSharp.Logging.Abstractions;
using ResultSharp.Tests.Helpers;
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
        }

        [TearDown]
        public void Reset()
        {
            mockLogger.Reset();
            ConfigurationHelpers.ResetGloabalConfiguration();
        }

        #endregion

        #region Configuration Test

        [Test]
        public void TryCallLogMethod_WhenLoggingIsDisable_ShouldIgnore()
        {
            ConfigurationHelpers.ResetGloabalConfiguration();
            new ResultConfigurationGlobal().Configure(options =>
            {
                options.EnableLogging = false;
            });

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
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
            result.LogErrorMessages();

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
            result.LogErrorMessages();

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

        #region Asynchronous Result Logging

        [Test]
        public async Task LogTraceAsync_ShouldCallLogWithTraceLevel()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogTraceAsync("Trace message");

            mockLogger.Verify(logger => logger.Log("Trace message", LogLevel.Trace, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogDebugAsync_ShouldCallLogWithDebugLevel()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogDebugAsync("Debug message");

            mockLogger.Verify(logger => logger.Log("Debug message", LogLevel.Debug, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogInformationAsync_ShouldCallLogWithInformationLevel()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogInformationAsync("Information message");

            mockLogger.Verify(logger => logger.Log("Information message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogWarningAsync_ShouldCallLogWithWarningLevel()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogWarningAsync("Warning message");

            mockLogger.Verify(logger => logger.Log("Warning message", LogLevel.Warning, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogErrorAsync_ShouldCallLogWithErrorLevel()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogErrorAsync("Error message");

            mockLogger.Verify(logger => logger.Log("Error message", LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogCriticalAsync_ShouldCallLogWithCriticalLevel()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogCriticalAsync("Critical message");

            mockLogger.Verify(logger => logger.Log("Critical message", LogLevel.Critical, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogIfSuccessAsync_ShouldLogWhenResultIsSuccess()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogIfSuccessAsync("Success message");

            mockLogger.Verify(logger => logger.Log("Success message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogIfSuccessAsync_ShouldNotLogWhenResultIsFailure()
        {
            var result = Task.FromResult(Result.Failure(new Error("Failure")));
            await result.LogIfSuccessAsync("Success message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        [Test]
        public async Task LogIfFailureAsync_ShouldLogWhenResultIsFailure()
        {
            var result = Task.FromResult(Result.Failure(new Error("Failure")));
            await result.LogIfFailureAsync("Failure message");

            mockLogger.Verify(logger => logger.Log("Failure message", LogLevel.Error, "ResultLogger", LogLevel.Error, It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogIfFailureAsync_ShouldNotLogWhenResultIsSuccess()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogIfFailureAsync("Failure message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        [Test]
        public async Task LogErrorMessagesAsync_ShouldLogErrorMessagesWhenResultIsFailure()
        {
            var result = Task.FromResult(Result.Failure(new Error("Failure")));
            await result.LogErrorMessagesAsync();

            mockLogger.Verify(logger => logger.Log("Failure", LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogErrorMessagesAsync_ShouldNotLogWhenResultIsSuccess()
        {
            var result = Task.FromResult(Result.Success());
            await result.LogErrorMessagesAsync();

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        [Test]
        public async Task LogTraceAsync_Generic_ShouldCallLogWithTraceLevel()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogTraceAsync("Trace message");

            mockLogger.Verify(logger => logger.Log("Trace message", LogLevel.Trace, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogDebugAsync_Generic_ShouldCallLogWithDebugLevel()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogDebugAsync("Debug message");

            mockLogger.Verify(logger => logger.Log("Debug message", LogLevel.Debug, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogInformationAsync_Generic_ShouldCallLogWithInformationLevel()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogInformationAsync("Information message");

            mockLogger.Verify(logger => logger.Log("Information message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogWarningAsync_Generic_ShouldCallLogWithWarningLevel()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogWarningAsync("Warning message");

            mockLogger.Verify(logger => logger.Log("Warning message", LogLevel.Warning, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogErrorAsync_Generic_ShouldCallLogWithErrorLevel()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogErrorAsync("Error message");

            mockLogger.Verify(logger => logger.Log("Error message", LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogCriticalAsync_Generic_ShouldCallLogWithCriticalLevel()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogCriticalAsync("Critical message");

            mockLogger.Verify(logger => logger.Log("Critical message", LogLevel.Critical, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogIfSuccessAsync_Generic_ShouldLogWhenResultIsSuccess()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogIfSuccessAsync("Success message");

            mockLogger.Verify(logger => logger.Log("Success message", LogLevel.Information, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogIfSuccessAsync_Generic_ShouldNotLogWhenResultIsFailure()
        {
            var result = Task.FromResult(Result<int>.Failure(new Error("Failure")));
            await result.LogIfSuccessAsync("Success message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        [Test]
        public async Task LogIfFailureAsync_Generic_ShouldLogWhenResultIsFailure()
        {
            var result = Task.FromResult(Result<int>.Failure(new Error("Failure")));
            await result.LogErrorMessagesAsync();

            mockLogger.Verify(logger => logger.Log("Failure", LogLevel.Error, "ResultLogger", It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task LogIfFailureAsync_Generic_ShouldNotLogWhenResultIsSuccess()
        {
            var result = Task.FromResult(Result<int>.Success(1));
            await result.LogIfFailureAsync("Failure message");

            mockLogger.Verify(logger => logger.Log(It.IsAny<string>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
        }

        #endregion
    }
}
