using NUnit.Framework;
using ResultSharp.Configuration.ExceptionHandler;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

namespace ResultSharp.Tests.Unit.Configuratoin.ExceptionHandler
{
    [TestFixture]
    public class ExceptionHandlerOptionsTests
    {
        [Test]
        public void ExceptionHandler_DefaultValue_ReturnsFailureResultWithExceptionMessage()
        {
            // Arrange
            var options = new ExceptionHandlerOptions();
            var exceptionMessage = "Test exception message";
            var exception = new Exception(exceptionMessage);

            // Act
            var result = options.ExceptionHandler(exception);

            // Assert
            Assert.AreEqual(exceptionMessage, result.Message);
            Assert.AreEqual(ErrorCode.Failure, result.ErrorCode);
        }

        [Test]
        public void ExceptionHandler_WhenSetToCustomHandler_UsesCustomHandler()
        {
            // Arrange
            var options = new ExceptionHandlerOptions();
            var customMessage = "Custom error message";
            var customCode = ErrorCode.NotFound;

            // Act
            options.ExceptionHandler = ex => new Error(customMessage, customCode);
            var result = options.ExceptionHandler(new Exception("Original message"));

            // Assert
            Assert.AreEqual(customMessage, result.Message);
            Assert.AreEqual(customCode, result.ErrorCode);
        }

        [Test]
        public void ExceptionHandler_WhenSetToNull_ThrowsArgumentNullException()
        {
            // Arrange
            var options = new ExceptionHandlerOptions();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => options.ExceptionHandler = null!);
            Assert.AreEqual("value", exception?.ParamName);
        }
    }
}