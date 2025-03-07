using NUnit.Framework;
using ResultSharp.Configuration.ExceptionHandler;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

namespace ResultSharp.Tests.Unit.Configuratoin.ExceptionHandler
{
    [TestFixture]
    public class ExceptionHandlerConfigurationTests
    {
        [Test]
        public void Configure_WhenCalledOnce_SetsConfiguration()
        {
            // Arrange
            var configuration = new ExceptionHandlerConfiguration();
            var customMessage = "Custom error message";

            // Act
            configuration.Configure(options =>
            {
                options.ExceptionHandler = ex => new Error(customMessage, ErrorCode.Failure);
            });

            // Assert
            Assert.IsTrue(configuration.IsConfigured);
            var result = configuration.Options.ExceptionHandler(new Exception("Test exception"));
            Assert.AreEqual(customMessage, result.Message);
            Assert.AreEqual(ErrorCode.Failure, result.ErrorCode);
        }

        [Test]
        public void Configure_WhenCalledMultipleTimes_ThrowsInvalidOperationException()
        {
            // Arrange
            var configuration = new ExceptionHandlerConfiguration();

            // Act
            configuration.Configure(options => { });

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                configuration.Configure(options => { }));
            Assert.AreEqual("ExceptionHandlerConfiguration configuration has already been set.", exception?.Message);
        }

        [Test]
        public void Configure_WhenConfigureActionIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var configuration = new ExceptionHandlerConfiguration();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => configuration.Configure(null!));
        }

        [Test]
        public void Configure_WithThreadSafety_WorksCorrectly()
        {
            // Arrange
            var configuration = new ExceptionHandlerConfiguration();
            var exceptions = new List<Exception>();

            // Act
            Parallel.For(0, 10, i =>
            {
                try
                {
                    configuration.Configure(options => { });
                }
                catch (Exception ex)
                {
                    lock (exceptions)
                    {
                        exceptions.Add(ex);
                    }
                }
            });

            // Assert
            Assert.IsTrue(configuration.IsConfigured);
            Assert.AreEqual(9, exceptions.Count);
            foreach (var ex in exceptions)
            {
                Assert.IsInstanceOf<InvalidOperationException>(ex);
            }
        }
    }
}