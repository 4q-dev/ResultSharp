using NUnit.Framework;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using ResultSharp.Extensions.TransformationExtensions;

namespace ResultSharp.Tests.Unit.Extensions
{
    [TestFixture]
    public class ToResultExtensionsTests
    {
        #region ToResult Methods

        [Test]
        public void ToResult_WithValue_ShouldReturnSuccessResult()
        {
            // Arrange
            var value = 10;

            // Act
            var result = value.ToResult();

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(value, result.Value);
        }

        [Test]
        public void ToResult_WithValueAndValidRules_ShouldReturnSuccessResult()
        {
            // Arrange
            var value = 10;
            var rules = new List<Predicate<int>> { v => v > 5, v => v < 20 };

            // Act
            var result = value.ToResult(rules);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(value, result.Value);
        }

        [Test]
        public void ToResult_WithValueAndInvalidRules_ShouldReturnFailureResult()
        {
            // Arrange
            var value = 10;
            var rules = new List<Predicate<int>> { v => v > 15, v => v < 5 };

            // Act
            var result = value.ToResult(rules);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCode.Validation, result.Errors.First().ErrorCode);
        }

        [Test]
        public void ToResult_WithValueAndValidationRules_WithAlternativeError_ShouldReturnAlternativeError()
        {
            // Arrange
            var value = 10;
            var rules = new List<Predicate<int>> { v => v > 15, v => v < 5 };
            var alternativeError = Error.NotFound();
         
            // Act
            var result = value.ToResult(rules, alternativeError);
            
            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCode.NotFound, result.Errors.First().ErrorCode);
        }

        #endregion

        #region ToResultAsync Methods

        [Test]
        public async Task ToResultAsync_WithTaskValue_ShouldReturnSuccessResult()
        {
            // Arrange
            var valueTask = Task.FromResult(10);

            // Act
            var result = await valueTask.ToResultAsync();

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task ToResultAsync_WithTaskValueAndValidRules_ShouldReturnSuccessResult()
        {
            // Arrange
            var valueTask = Task.FromResult(10);
            var rules = new List<Predicate<int>> { v => v > 5, v => v < 20 };

            // Act
            var result = await valueTask.ToResultAsync(rules);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task ToResultAsync_WithTaskValueAndInvalidRules_ShouldReturnFailureResult()
        {
            // Arrange
            var valueTask = Task.FromResult(10);
            var rules = new List<Predicate<int>> { v => v > 15, v => v < 5 };

            // Act
            var result = await valueTask.ToResultAsync(rules);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(ErrorCode.Validation, result.Errors.First().ErrorCode);
        }

        #endregion
    }
}

