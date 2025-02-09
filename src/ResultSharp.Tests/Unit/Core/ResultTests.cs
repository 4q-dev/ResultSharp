using NUnit.Framework;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using System.Collections.ObjectModel;

namespace ResultSharp.Tests.Unit.Core
{
    [TestFixture]
    public class ResultTests
    {
        #region Success / Failure

        [Test]
        public void Success_ShouldReturnSuccessfulResultWithNoErrors()
        {
            var result = Result.Success();

            Assert.IsTrue(result.IsSuccess, "Result должен быть успешным.");
        }

        [Test]
        public void Failure_WithParams_ShouldReturnFailureResultWithErrors()
        {
            var error = new Error("Ошибка", ErrorCodes.Failure);

            var result = Result.Failure(error);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.AreEqual(1, result.Errors.Count, "Количество ошибок должно быть равно 1.");
            Assert.That(result.Errors.First().Message.Contains("Ошибка"));
        }

        [Test]
        public void Failure_WithIEnumerable_ShouldReturnFailureResultWithErrors()
        {
            var errors = new List<Error>
            {
                new Error("Ошибка 1", ErrorCodes.Failure),
                new Error("Ошибка 2", ErrorCodes.Failure)
            };

            var result = Result.Failure(errors);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.AreEqual(2, result.Errors.Count, "Количество ошибок должно быть равно 2.");
        }

        #endregion

        #region Merge

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
            var error1 = new Error("Ошибка A", ErrorCodes.Failure);
            var error2 = new Error("Ошибка B", ErrorCodes.Failure);
            var failureResult1 = Result.Failure(error1);
            var failureResult2 = Result.Failure(error2);

            var merged = Result.Merge(successResult, failureResult1, failureResult2);

            Assert.IsFalse(merged.IsSuccess, "Если хотя бы один результат неуспешный, итоговый результат должен быть Failure.");
            Assert.AreEqual(2, merged.Errors.Count, "Количество агрегированных ошибок должно быть равно 2.");
            Assert.That(merged.Errors.Any(e => e.Message.Contains("Ошибка A")));
            Assert.That(merged.Errors.Any(e => e.Message.Contains("Ошибка B")));
        }

        #endregion

        #region Implicit Conversions

        [Test]
        public void ImplicitConversion_FromError_ShouldReturnFailureResult()
        {
            var error = new Error("Implicit error", ErrorCodes.Failure);

            Result result = error; // implicit conversion

            Assert.IsFalse(result.IsSuccess, "Результат должен быть неуспешным после implicit-конверсии из Error.");
            Assert.AreEqual(1, result.Errors.Count);
            Assert.That(result.Errors.ElementAt(0).Message.Contains("Implicit error"));
        }

        [Test]
        public void ImplicitConversion_FromListOfErrors_ShouldReturnFailureResult()
        {
            var errors = new List<Error>
            {
                new Error("List error 1", ErrorCodes.Failure),
                new Error("List error 2", ErrorCodes.Failure)
            };

            Result result = errors; // implicit conversion

            Assert.IsFalse(result.IsSuccess, "Результат должен быть неуспешным при implicit-конверсии из List<Error>.");
            Assert.AreEqual(2, result.Errors.Count);
        }

        [Test]
        public void ImplicitConversion_FromArrayOfErrors_ShouldReturnFailureResult()
        {
            var errors = new Error[]
            {
                new Error("Array error 1", ErrorCodes.Failure),
                new Error("Array error 2", ErrorCodes.Failure)
            };

            Result result = errors; // implicit conversion

            Assert.IsFalse(result.IsSuccess, "Результат должен быть неуспешным при implicit-конверсии из массива ошибок.");
            Assert.AreEqual(2, result.Errors.Count);
        }

        [Test]
        public void ImplicitConversion_ToReadOnlyCollectionOfError_ShouldReturnErrorCollection()
        {
            var error = new Error("Conversion error", ErrorCodes.Failure);
            var result = Result.Failure(error);

            ReadOnlyCollection<Error> errors = result; // implicit conversion

            Assert.IsNotNull(errors);
            Assert.AreEqual(1, errors.Count);
            Assert.That(errors.ElementAt(0).Message.Contains("Conversion error"));
        }

        #endregion

        #region Summary Error Messages

        [Test]
        public void SummaryErrorMessages_ThrowException_WhenNoErrors()
        {
            var result = Result.Success();
            Assert.Throws<InvalidOperationException>(() => result.SummaryErrorMessages());
        }

        [Test]
        public void SummaryErrorMessages_ReturnsConcatenatedMessages_WhenErrorsExist()
        {
            var error1 = new Error("Error1", ErrorCodes.Failure);
            var error2 = new Error("Error2", ErrorCodes.Failure);
            var result = Result.Failure(error1, error2);
            var summary = result.SummaryErrorMessages();
            var expected = "Error1" + Environment.NewLine + "Error2";
            Assert.AreEqual(expected, summary);
        }

        #endregion
    }
}
