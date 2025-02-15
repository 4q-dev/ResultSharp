using NUnit.Framework;
using ResultSharp.Core;
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
            var error = new Error("An error occurred", ErrorCode.Failure);
            var result = Result<int>.Failure(error);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.Throws<InvalidOperationException>(() => { var v = result.Value; },
                "При попытке доступа к Value в неуспешном результате должно выбрасываться исключение.");
        }

        [Test]
        public void FailureResult_Should_ContainMultipleErrors()
        {
            var error1 = new Error("Error 1", ErrorCode.Validation);
            var error2 = new Error("Error 2", ErrorCode.Failure);
            var result = Result<int>.Failure(error1, error2);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.AreEqual(2, result.Errors.Count, "Количество ошибок не соответствует ожидаемому.");
            Assert.That(result.Errors.Any(e => e.Message.Contains("Error 1")), "Ошибка 'Error 1' отсутствует.");
            Assert.That(result.Errors.Any(e => e.Message.Contains("Error 2")), "Ошибка 'Error 2' отсутствует.");
        }

        [Test]
        public void FailureResult_WithIEnumerable_Should_ContainErrors()
        {
            var error1 = new Error("Error A", ErrorCode.Failure);
            var error2 = new Error("Error B", ErrorCode.Failure);
            var errors = new List<Error> { error1, error2 };
            var result = Result<int>.Failure(errors);

            Assert.IsFalse(result.IsSuccess, "Result должен быть неуспешным.");
            Assert.AreEqual(2, result.Errors.Count, "Количество ошибок не соответствует ожидаемому.");
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
            Error error = new Error("Implicit error", ErrorCode.Failure);
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
                new Error("List error 1", ErrorCode.Failure),
                new Error("List error 2", ErrorCode.Failure)
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
                new Error("Array error 1", ErrorCode.Failure),
                new Error("Array error 2", ErrorCode.Failure)
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
            var error = new Error("Test error", ErrorCode.Failure);
            var result = Result<double>.Failure(error);
            ReadOnlyCollection<Error> errors = result;
            Assert.AreEqual(1, errors.Count);
            Assert.That(errors.ElementAt(0).Message.Contains("Test error"));
        }

        #endregion
    }
}
