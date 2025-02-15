using NUnit.Framework;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

namespace ResultSharp.Tests.Unit.Core
{
    [TestFixture]
    internal class TryTests
    {
        #region Try Methods

        [Test]
        public void Try_CapturesExceptionsAndReturnsFailure()
        {
            var result = Result.Try(
                () => throw new InvalidOperationException("Test exception"),
                ex => new Error(ex.Message, ErrorCode.InternalServerError)
            );

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Test exception", result.Errors.ElementAt(0).Message);
        }

        [Test]
        public void Try_ExecutesSuccessfully_ReturnsSuccess()
        {
            var result = Result.Try(() => { /* No exception */ }, ex => new Error(ex.Message));
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Try_GenericCapturesExceptionsAndReturnsFailure()
        {
            var result = Result.Try(
                () => true ? throw new InvalidOperationException("Test exception") : 10,
                ex => new Error(ex.Message, ErrorCode.InternalServerError)
            );

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Test exception", result.Errors.ElementAt(0).Message);
        }

        [Test]
        public void Try_ExecutesSuccessfully_ReturnsGenericSuccess()
        {
            var result = Result.Try(() => { return 10; }, ex => new Error(ex.Message));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value, 10);
        }

        #endregion

        #region Try Async Methods

        [Test]
        public async Task TryAsync_WithSuccessfulFunction_ShouldReturnSuccess()
        {
            // Arrange
            Func<Task> func = async () => await Task.Delay(10);
            Func<Exception, Error> handler = ex => Error.Failure(ex.Message);

            // Act
            var result = await Result.TryAsync(func, handler);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task TryAsync_WithException_ShouldReturnFailureWithHandledError()
        {
            // Arrange
            Func<Task> func = async () =>
            {
                await Task.Delay(10);
                throw new InvalidOperationException("Test exception");
            };
            Func<Exception, Error> handler = ex => Error.Failure(ex.Message);

            // Act
            var result = await Result.TryAsync(func, handler);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Test exception", result.Errors.First().Message);
        }

        [Test]
        public async Task TryAsync_Generic_WithSuccessfulFunction_ShouldReturnSuccess()
        {
            // Arrange
            Func<Task<int>> func = async () =>
            {
                await Task.Delay(10);
                return 42;
            };
            Func<Exception, Error> handler = ex => Error.Failure(ex.Message);

            // Act
            var result = await Result.TryAsync(func, handler);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(42, result.Value);
        }

        [Test]
        public async Task TryAsync_Generic_WithException_ShouldReturnFailureWithHandledError()
        {
            // Arrange
            Func<Task<int>> func = async () =>
            {
                await Task.Delay(10);
                throw new InvalidOperationException("Test exception");
            };
            Func<Exception, Error> handler = ex => Error.Failure(ex.Message);

            // Act
            var result = await Result.TryAsync(func, handler);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Test exception", result.Errors.First().Message);
        }

        #endregion
    }
}
