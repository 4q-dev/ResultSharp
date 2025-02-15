using NUnit.Framework;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

namespace ResultSharp.Tests.Unit.Core
{
    [TestFixture]
    internal class MergeTests
    {
        #region Result Merge

        [Test]
        public void Merge_AllSuccessResults_ShouldReturnSuccessfulResult()
        {
            var r1 = Result.Success();
            var r2 = Result.Success();
            var r3 = Result.Success();

            var merged = Result.Merge(r1, r2, r3);

            Assert.IsTrue(merged.IsSuccess, "Объединённый результат должен быть успешным, если все результаты успешны.");
        }

        [Test]
        public void Merge_AtLeastOneFailure_ShouldReturnFailureResultWithAggregatedErrors()
        {
            var successResult = Result.Success();
            var error1 = new Error("Ошибка A", ErrorCode.Failure);
            var error2 = new Error("Ошибка B", ErrorCode.Failure);
            var failureResult1 = Result.Failure(error1);
            var failureResult2 = Result.Failure(error2);

            var merged = Result.Merge(successResult, failureResult1, failureResult2);

            Assert.IsFalse(merged.IsSuccess, "Если хотя бы один результат неуспешный, итоговый результат должен быть Failure.");
            Assert.AreEqual(2, merged.Errors.Count, "Количество агрегированных ошибок должно быть равно 2.");
            Assert.That(merged.Errors.Any(e => e.Message.Contains("Ошибка A")));
            Assert.That(merged.Errors.Any(e => e.Message.Contains("Ошибка B")));
        }

        #endregion

        #region Generic Result Merge

        [Test]
        public void Merge_SuccessResults_Should_ReturnMergedSuccess()
        {
            var r1 = Result<int>.Success(10);
            var r2 = Result<int>.Success(20);
            var r3 = Result<int>.Success(30);

            var merged = Result<int>.Merge(r1, r2, r3);

            Assert.IsTrue(merged.IsSuccess, "Объединённый результат должен быть успешным.");
            var values = merged.Value;
            CollectionAssert.AreEqual(new[] { 10, 20, 30 }, values, "Объединённые значения не соответствуют ожидаемым.");
        }

        [Test]
        public void Merge_FailureResults_Should_ReturnFailureWithErrors()
        {
            var error1 = new Error("Merge Error 1", ErrorCode.Failure);
            var error2 = new Error("Merge Error 2", ErrorCode.Failure);
            var r1 = Result<int>.Success(100);
            var r2 = Result<int>.Failure(error1);
            var r3 = Result<int>.Failure(error2);

            var merged = Result<int>.Merge(r1, r2, r3);

            Assert.IsFalse(merged.IsSuccess, "Объединённый результат должен быть неуспешным.");
            Assert.AreEqual(2, merged.Errors.Count, "Количество ошибок не соответствует ожидаемому.");
            Assert.That(merged.Errors.Any(e => e.Message.Contains("Merge Error 1")));
            Assert.That(merged.Errors.Any(e => e.Message.Contains("Merge Error 2")));
        }

        [Test]
        public void Merge_Generic_TwoTypes_Success_Should_ReturnSuccess()
        {
            var r1 = Result<int>.Success(10);
            var r2 = Result<string>.Success("zaza");

            var merged = Result<object>.Merge(r1, r2);

            Assert.IsTrue(merged.IsSuccess, "Объединённый результат должен быть успешным.");
        }

        [Test]
        public void Merge_Generic_Failure_Should_ReturnFailureWithErrors()
        {
            var error = new Error("Generic error", ErrorCode.Failure);
            var r1 = Result<object>.Failure(error);
            var r2 = Result<object>.Success(new object());

            var merged = Result<object>.Merge(r1, r2);

            Assert.IsFalse(merged.IsSuccess, "Объединённый результат должен быть неуспешным.");
            Assert.AreEqual(1, merged.Errors.Count, "Количество ошибок не соответствует ожидаемому.");
            Assert.That(merged.Errors.ElementAt(0).Message.Contains("Generic error"));
        }

        #endregion

        #region Async Result Merge

        [Test]
        public async Task MergeAsync_WithAllSuccessResults_ShouldReturnSuccess()
        {
            // Arrange
            var result1 = Task.FromResult(Result.Success());
            var result2 = Task.FromResult(Result.Success());
            var result3 = Task.FromResult(Result.Success());

            // Act
            var mergedResult = await Result.MergeAsync(result1, result2, result3);

            // Assert
            Assert.IsTrue(mergedResult.IsSuccess);
        }

        [Test]
        public async Task MergeAsync_WithFailureResults_ShouldReturnFailureWithErrors()
        {
            // Arrange
            var error1 = Error.Failure("Error 1");
            var error2 = Error.Failure("Error 2");
            var result1 = Task.FromResult(Result.Failure(error1));
            var result2 = Task.FromResult(Result.Success());
            var result3 = Task.FromResult(Result.Failure(error2));

            // Act
            var mergedResult = await Result.MergeAsync(result1, result2, result3);

            // Assert
            Assert.IsTrue(mergedResult.IsFailure);
            CollectionAssert.AreEqual(new[] { error1, error2 }, mergedResult.Errors);
        }

        #endregion

        #region Async Generic Result Merge

        [Test]
        public async Task MergeAsync_WithAllSuccessGenericResults_ShouldReturnSuccessWithValues()
        {
            // Arrange
            var result1 = Task.FromResult(Result<int>.Success(1));
            var result2 = Task.FromResult(Result<int>.Success(2));
            var result3 = Task.FromResult(Result<int>.Success(3));

            // Act
            var mergedResult = await Result<int>.MergeAsync(result1, result2, result3);

            // Assert
            Assert.IsTrue(mergedResult.IsSuccess);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, mergedResult.Value);
        }

        [Test]
        public async Task MergeAsync_WithFailureGenericResults_ShouldReturnFailureWithErrors()
        {
            // Arrange
            var error1 = Error.Failure("Error 1");
            var error2 = Error.Failure("Error 2");
            var result1 = Task.FromResult(Result<int>.Failure(error1));
            var result2 = Task.FromResult(Result<int>.Success(2));
            var result3 = Task.FromResult(Result<int>.Failure(error2));

            // Act
            var mergedResult = await Result<int>.MergeAsync(result1, result2, result3);

            // Assert
            Assert.IsTrue(mergedResult.IsFailure);
            CollectionAssert.AreEqual(new[] { error1, error2 }, mergedResult.Errors);
        }

        #endregion
    }
}
