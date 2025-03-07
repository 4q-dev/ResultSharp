using NUnit.Framework;
using ResultSharp.Configuration;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using ResultSharp.Tests.Helpers;

namespace ResultSharp.Tests.Unit.Core
{
    [TestFixture]
    public class ResultTryDefaultHandlerTests
    {

        [SetUp]
        public void SetUpOnce()
        {
            new ResultConfigurationGlobal().Configure(options => options.EnableLogging = false);
        }

        [TearDown]
        public void TearDown()
        {
            ConfigurationHelpers.ResetGloabalConfiguration();
        }

        [Test]
        public void Try_WithDefaultHandler_UsesConfiguredHandler()
        {
            // Act
            var result = Result.Try(() => throw new Exception("Тестовое исключение"));

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Тестовое исключение", result.Errors.First().Message);
            Assert.AreEqual(ErrorCode.Failure, result.Errors.First().ErrorCode);
        }

        [Test]
        public async Task TryAsync_WithDefaultHandler_UsesConfiguredHandler()
        {
            // Act
            var result = await Result.TryAsync(async () =>
            {
                await Task.Delay(10);
                throw new Exception("Тестовое исключение");
            }, null);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Тестовое исключение", result.Errors.First().Message);
            Assert.AreEqual(ErrorCode.Failure, result.Errors.First().ErrorCode);
        }

        [Test]
        public void TryWithResult_WithDefaultHandler_UsesConfiguredHandler()
        {
            var result = Result.Try<int>(() => throw new Exception("Тестовое исключение"), null);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Тестовое исключение", result.Errors.First().Message);
            Assert.AreEqual(ErrorCode.Failure, result.Errors.First().ErrorCode);
        }

        [Test]
        public async Task TryWithResultAsync_WithDefaultHandler_UsesConfiguredHandler()
        {

            // Act
            var result = await Result.TryAsync<int>(async () =>
            {
                await Task.Delay(10);
                throw new Exception("Тестовое исключение");
            }, null);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Тестовое исключение", result.Errors.First().Message);
            Assert.AreEqual(ErrorCode.Failure, result.Errors.First().ErrorCode);
        }

        [Test]
        public void Try_WithDefaultHandlerAndDifferentExceptionTypes_HandlesAllExceptions()
        {
            // Arrange
            ConfigurationHelpers.ResetGloabalConfiguration();
            new ResultConfigurationGlobal().Configure(options =>
            {
                options.EnableLogging = false;
                options.ExceptionHandlerConfiguration.Configure(options =>
                {
                    options.ExceptionHandler = ex =>
                    {
                        var errorCode = ex switch
                        {
                            ArgumentException => ErrorCode.Validation,
                            InvalidOperationException => ErrorCode.Conflict,
                            _ => ErrorCode.Failure
                        };

                        return new Error(ex.Message, errorCode);
                    };
                });
            });

            // Act
            var result1 = Result.Try(() => throw new ArgumentException("Неверный аргумент"));
            var result2 = Result.Try(() => throw new InvalidOperationException("Неверная операция"));
            var result3 = Result.Try(() => throw new Exception("Общее исключение"));

            // Assert
            Assert.IsFalse(result1.IsSuccess);
            Assert.AreEqual(ErrorCode.Validation, result1.Errors.First().ErrorCode);

            Assert.IsFalse(result2.IsSuccess);
            Assert.AreEqual(ErrorCode.Conflict, result2.Errors.First().ErrorCode);

            Assert.IsFalse(result3.IsSuccess);
            Assert.AreEqual(ErrorCode.Failure, result3.Errors.First().ErrorCode);
        }

        [Test]
        public void Try_WithoutConfiguredHandler_UsesDefaultImplementation()
        {

            // Act
            var exceptionMessage = "Тестовое исключение";
            var result = Result.Try(() => throw new Exception(exceptionMessage));

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(exceptionMessage, result.Errors.First().Message);
            Assert.AreEqual(ErrorCode.Failure, result.Errors.First().ErrorCode);
        }

        [Test]
        public void Try_WithSuccessfulExecution_ReturnsSuccessResult()
        {
            // Act
            var result = Result.Try(() => { /* успешное выполнение */ });

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void TryWithResult_WithSuccessfulExecution_ReturnsSuccessResultWithValue()
        {
            // Act
            var expectedValue = 42;
            var result = Result.Try(() => expectedValue, null);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(expectedValue, result.Value);
        }
    }
}