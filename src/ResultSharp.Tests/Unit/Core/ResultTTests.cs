using NUnit.Framework;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using System.Collections.ObjectModel;

namespace ResultSharp.Tests.Unit.Core
{
    [TestFixture]
    public class ResultTTests
    {
        #region Success / Failure

        [Test]
        public void SuccessResult_Should_HaveValueAndBeSuccessful()
        {
            int expected = 42;
            var result = Result<int>.Success(expected);

            Assert.IsTrue(result.IsSuccess, "Result должен быть успешным.");
            Assert.AreEqual(expected, result.Value, "Значение результата не соответствует ожидаемому.");
        }

        [Test]
        public void FailureResult_Should_BeFailureAndThrowOnValueAccess()
        {
            var error = new Error("An error occurred", ErrorCodes.Failure);
            var result = Result<int>.Failure(error);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.Throws<InvalidOperationException>(() => { var v = result.Value; },
                "При попытке доступа к Value в неуспешном результате должно выбрасываться исключение.");
        }

        [Test]
        public void FailureResult_Should_ContainMultipleErrors()
        {
            var error1 = new Error("Error 1", ErrorCodes.Validation);
            var error2 = new Error("Error 2", ErrorCodes.Failure);
            var result = Result<int>.Failure(error1, error2);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.AreEqual(2, result.Errors.Count, "Количество ошибок не соответствует ожидаемому.");
            Assert.That(result.Errors.Any(e => e.Message.Contains("Error 1")), "Ошибка 'Error 1' отсутствует.");
            Assert.That(result.Errors.Any(e => e.Message.Contains("Error 2")), "Ошибка 'Error 2' отсутствует.");
        }

        [Test]
        public void FailureResult_WithIEnumerable_Should_ContainErrors()
        {
            var error1 = new Error("Error A", ErrorCodes.Failure);
            var error2 = new Error("Error B", ErrorCodes.Failure);
            var errors = new List<Error> { error1, error2 };
            var result = Result<int>.Failure(errors);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.AreEqual(2, result.Errors.Count, "Количество ошибок не соответствует ожидаемому.");
        }

        #endregion

        #region Merge

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
            var error1 = new Error("Merge Error 1", ErrorCodes.Failure);
            var error2 = new Error("Merge Error 2", ErrorCodes.Failure);
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
            var error = new Error("Generic error", ErrorCodes.Failure);
            var r1 = Result<object>.Failure(error);
            var r2 = Result<object>.Success(new object());

            var merged = Result<object>.Merge(r1, r2);

            Assert.IsFalse(merged.IsSuccess, "Объединённый результат должен быть неуспешным.");
            Assert.AreEqual(1, merged.Errors.Count, "Количество ошибок не соответствует ожидаемому.");
            Assert.That(merged.Errors.ElementAt(0).Message.Contains("Generic error"));
        }

        #endregion

        #region Implicit Conversion

        [Test]
        public void ImplicitConversion_FromTResult_To_ResultT()
        {
            Result<int> result = 55;
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(55, result.Value);
        }

        [Test]
        public void ImplicitConversion_FromError_To_ResultT()
        {
            Error error = new Error("Implicit error", ErrorCodes.Failure);
            Result<int> result = error;
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.That(result.Errors.ElementAt(0).Message.Contains("Implicit error"));
        }

        [Test]
        public void ImplicitConversion_FromListOfErrors_To_ResultT()
        {
            var errors = new List<Error>
            {
                new Error("List error 1", ErrorCodes.Failure),
                new Error("List error 2", ErrorCodes.Failure)
            };
            Result<int> result = errors;
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(2, result.Errors.Count);
        }

        [Test]
        public void ImplicitConversion_FromArrayOfErrors_To_ResultT()
        {
            var errors = new Error[]
            {
                new Error("Array error 1", ErrorCodes.Failure),
                new Error("Array error 2", ErrorCodes.Failure)
            };
            Result<int> result = errors;
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(2, result.Errors.Count);
        }

        [Test]
        public void ImplicitConversion_ToTResult_From_ResultT_Success()
        {
            var result = Result<string>.Success("Hello");
            string value = result;
            Assert.AreEqual("Hello", value);
        }

        [Test]
        public void ImplicitConversion_ToReadOnlyCollectionOfError_From_ResultT_Failure()
        {
            var error = new Error("Test error", ErrorCodes.Failure);
            var result = Result<double>.Failure(error);
            ReadOnlyCollection<Error> errors = result;
            Assert.AreEqual(1, errors.Count);
            Assert.That(errors.ElementAt(0).Message.Contains("Test error"));
        }

        #endregion
    }
}
