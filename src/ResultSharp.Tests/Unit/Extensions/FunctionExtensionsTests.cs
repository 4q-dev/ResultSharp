using NUnit.Framework;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using ResultSharp.Extensions.FunctionalExtensions.Sync;
using System.Collections.ObjectModel;


#region овсянка, если ты это смотришь, это спецально для тебя:
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣯⣾⣿⡿⢟⣿⠛⠉⠩⠁⠀⠀⡟⠁⠀⣀⠀⠀⠈⠙⠿⣿⣿⣿⣿⣿⣿⣿⣿⣦⠀⠀⠀⠀⠀⠀⠈⠢⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣾⡿⠟⠁⣐⡮⠁⠀⡐⠀⠀⠀⠀⢰⣅⠈⠀⠒⠄⡀⠀⠀⠀⠙⠿⣿⣿⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠐⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣰⡿⠋⠀⠀⠀⡬⠁⠀⡐⠀⠀⠀⠀⠀⠀⠟⢂⠀⠀⠀⠈⠂⡀⠀⠀⠀⠈⠙⢿⣿⣻⣿⣿⣿⣷⡀⠀⠀⠀⠀⠀⠈⢳⡀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣲⠏⠀⠀⠀⠠⡙⠀⠀⢀⠁⠀⠀⠀⠀⠀⠀⠐⡈⠑⡀⠀⠀⠀⠈⠢⡀⠀⠀⠀⠀⠙⣿⣿⣿⣿⣿⣷⡀⠀⠀⠀⠀⠀⠀⢗⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠬⠁⠀⢀⠈⠀⢠⠁⠀⠀⡈⠀⠀⠀⠀⠀⠀⠀⠀⠐⡀⠈⢄⠀⠀⠀⠀⠀⢄⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣷⡀⠀⠀⠀⠀⠀⠈⠦⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⠀⠀⠂⠀⠀⡄⠀⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⠈⢂⠀⠀⠀⠀⠀⢂⠀⠀⠀⠀⠀⠈⢿⣿⣿⣿⣗⡀⠀⠀⠀⠀⠀⠈⣇⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⣨⠁⠀⠀⠄⠀⠀⢀⠀⠀⠀⢰⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢂⠀⠀⠀⠀⠀⢂⠀⠀⠀⠀⠀⠀⢻⣿⣿⣿⡰⠀⠀⠀⠀⠀⠀⢩⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⢠⡇⠀⠀⡘⠀⠀⠀⢸⠀⠀⠀⢸⠀⠀⠀⠀⠀⠀⣣⠀⠀⠀⠀⠀⠀⠀⠀⢆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣿⣿⡇⢃⠀⠀⠀⠀⠀⠸⢆⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⣨⠀⠀⢀⠁⠀⠀⠀⠀⠀⠀⠀⣼⠀⠀⠀⠀⠀⠀⢁⢀⠀⠀⠀⠀⠀⠀⠀⠈⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿⡇⠋⡀⠀⠀⠀⠀⠀⣏⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⢠⡇⠀⠀⡈⠀⠀⠀⠀⠀⠀⠰⢰⣿⡆⠀⠀⠀⠀⠀⠀⠄⢂⠀⠐⡀⠀⠀⠀⠀⠈⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⣿⠈⢡⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⢨⠀⠀⢀⠁⠀⠀⠀⠀⢀⠀⠆⣿⣿⣧⠀⠠⠀⠐⠀⠀⠘⡀⠠⠀⠐⣀⠀⠀⠀⠀⠘⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⠀⠈⡄⠀⠀⠀⠀⠘⠆⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⣿⠀⠀⠘⠀⠀⠀⠀⠀⠸⠰⢸⣿⣿⣿⣆⠀⢂⠀⠀⠀⠀⠐⡀⠐⠀⠐⠑⠀⠀⠈⠄⠐⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⠀⠀⢁⠀⠀⠀⠀⠀⡆⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⡊⠀⠀⡀⠀⠀⠀⠀⠀⢐⢳⣾⣿⣿⣿⣿⡌⠌⣆⠀⠀⠀⠀⠐⠀⠈⠂⠈⢀⠢⡀⠀⢂⢡⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⠀⠀⠘⠀⠀⠀⠀⠀⠣⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⡅⠀⠀⡇⠀⡀⠀⠀⠀⢰⣧⣿⣿⣿⣿⣿⣿⣄⠚⢆⠀⠀⠀⠀⠈⢄⠀⠑⠀⠁⡀⠀⠀⠂⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⡆⠀⠀⡆⡆⡔⠄⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⣿⠀⠃⢸⢀⡇⠀⠀⠠⠈⣻⣿⣿⣿⣿⣿⣿⣿⣦⠙⡧⡀⠀⠀⠀⠀⠀⡀⠀⠀⠈⠀⡀⢀⢲⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⢸⠆⠀⠀⡇⢰⠀⢳⠁⢷⠗⡀⠀⠀
            ⠀⠀⠀⠀⠀⠀⢹⢰⠀⠈⣸⢰⠀⠀⠀⠒⡹⣿⣿⣿⣿⣿⣿⢿⣿⣷⣜⣮⣦⠀⠀⠀⠀⠈⠊⠀⠀⠀⢄⠀⠀⡄⠀⠀⠀⠀⠀⠀⠀⠀⢤⢸⠀⠀⠀⡇⢸⢰⣾⢠⠆⠁⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠸⢸⠆⠀⢡⢀⠃⠀⠀⢀⠱⡹⣿⣿⣿⣿⣯⣿⣼⣹⣻⣮⣿⣿⡦⡀⠐⠄⠀⠈⠢⡢⢀⠀⠠⡣⡀⠀⠀⠀⠀⠀⠀⠀⡿⢸⠀⠀⠀⡇⠸⢘⠙⣾⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⢸⣾⣯⣶⠀⢸⠎⠄⡀⠈⡷⣼⣌⢻⣿⣿⣿⠿⢻⠩⠁⠀⠈⠑⢖⠠⠔⢄⠀⢌⠐⠨⠢⠁⠣⠂⠰⢠⣄⠀⠀⠀⠀⢠⡙⡈⠀⠀⠀⣌⢀⢸⠀⠏⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⢹⣁⠱⡇⢸⢰⡘⢇⢄⢡⠈⢿⣷⠟⠋⠀⠀⠀⢅⠀⠀⠀⠀⠀⠁⠀⠀⠈⠂⡝⡰⢠⠄⣐⡆⠀⢀⠀⠈⠀⠂⢠⢲⢡⠃⠀⠀⣰⠂⣸⡆⠘⠃⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠙⠦⢩⣆⢼⢡⠈⠂⠱⢕⠌⡅⠀⢀⠀⠀⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠂⠁⠊⠔⣣⣀⣀⣀⠸⠀⣠⢏⡲⡉⠀⠠⢠⡏⢠⣗⠁⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢧⢯⣛⣥⠀⠀⠂⠈⠩⢊⠀⠀⠀⠀⠀⢀⡀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢁⣵⠉⠁⠀⠄⡆⡊⣜⡚⡔⠀⢠⡵⣿⢠⠯⠂⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢊⡎⠙⢓⠄⠀⢆⠀⠑⠱⢄⠀⠀⠀⠁⢃⡀⠀⠉⠀⠀⠀⠀⠀⠀⢀⢐⠚⠹⠀⡀⣨⢰⡀⢨⡣⡞⣠⡰⠉⢀⣭⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢧⠤⠢⠽⠦⢈⠑⢦⢀⠳⡅⡠⡀⠀⠀⠀⠀⠀⠁⠀⠀⠀⡠⣐⠁⡂⢀⢃⣔⣔⣇⢛⢣⡜⣷⠮⡧⠃⠈⢐⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⡆⠀⢠⠈⠑⠊⣾⣺⢳⣿⠙⢲⣄⡀⠀⢀⣀⣤⡔⢻⡔⠃⠁⣀⡔⢹⡏⢹⣿⠁⠙⣷⣟⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⡄⣴⠾⠟⣴⢠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠀⠟⠃⣿⣿⡅⠘⠈⠉⠉⠁⠀⠀⠀⠃⠛⠃⢸⣷⣼⡏⠀⠀⠈⠻⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⣠⠤⢓⣵⣾⣿⣷⣄⠈⢘⠲⣠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⣿⣷⣶⣶⣶⣤⣤⣤⣤⣶⣶⣶⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⢀⡤⠓⠁⣰⡿⣽⢾⣟⣿⣿⣧⡄⡘⠢⡟⣧⣠⡀⠀⠀⢀⡀⣀⣴⣁⠟⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣅⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠟⠁⢀⣾⢯⣟⣽⣻⣞⡿⣿⣿⣿⣮⣳⣜⣻⣿⣷⠒⠊⠉⠁⣠⣴⣶⣦⣤⣙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣗⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⢤⣤⣠⠀⠀⠀⠀⠀
            ⠀⡠⢟⣞⣯⣞⣷⣻⣾⣿⣿⣿⣿⣿⣿⣾⣷⣿⢿⣵⣤⣶⣿⣿⡿⣿⣿⣿⣿⣾⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡴⣠⠀⢀⣠⠠⠒⠒⠒⠒⠋⣰⣿⡿⣿⣷⣴⠀⠀⠀
            ⢀⠝⣸⣿⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢯⣟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡐⣉⠯⣄⠀⠀⠀⠀⠀⣴⣿⢯⣿⢷⣿⣿⣷⡂⠀
            ⢈⢠⣿⣽⣻⣿⢿⣯⣿⣽⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣵⣫⡽⣷⠀⢀⣠⣾⡿⣯⣿⣾⣿⣿⣟⣿⣿⡄
            ⠀⣼⣷⣻⣿⣾⣿⣯⣿⣯⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⡽⣧⠛⠿⣿⣽⣿⣿⣿⣿⣿⣿⣿⣿⣿
            ⢠⣿⡽⣟⣷⣿⣿⣿⣷⣿⡿⣿⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣯⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣬⣟⣯⣿⣿⣿⣿⣿⣿⣿⣿
*/

#endregion

namespace ResultSharp.Tests.Unit.Extensions
{
    [TestFixture]
    public class FunctionExtensionsTests
    {

        #region Match Methods

        [Test]
        public void Match_SuccessfulResult_CallsOnSuccess()
        {
            var result = Result.Success();
            bool successCalled = false, failureCalled = false;

            result.Match(
                onSuccess: () => successCalled = true,
                onFailure: () => failureCalled = true
            );

            Assert.IsTrue(successCalled);
            Assert.IsFalse(failureCalled);
        }

        [Test]
        public void Match_FailedResult_CallsOnFailure()
        {
            var error = new Error("Something went wrong", ErrorCode.InternalServerError);
            var result = Result.Failure(error);
            bool successCalled = false, failureCalled = false;

            result.Match(
                onSuccess: () => successCalled = true,
                onFailure: () => failureCalled = true
            );

            Assert.IsFalse(successCalled);
            Assert.IsTrue(failureCalled);
        }

        [Test]
        public void Match_ResultWithValue_Success_CallsOnSuccessWithValue()
        {
            var result = Result<int>.Success(42);
            int capturedValue = 0;

            result.Match(
                onSuccess: value => capturedValue = value,
                onFailure: errors => Assert.Fail("Should not be called")
            );

            Assert.AreEqual(42, capturedValue);
        }

        [Test]
        public void Match_ResultWithValue_Failure_CallsOnFailureWithErrors()
        {
            var error = new Error("Invalid input", ErrorCode.Validation);
            var result = Result<int>.Failure(error);
            ReadOnlyCollection<Error>? capturedErrors = null;

            result.Match(
                onSuccess: value => Assert.Fail("Should not be called"),
                onFailure: errors => capturedErrors = errors
            );

            Assert.IsNotNull(capturedErrors);
            Assert.AreEqual(1, capturedErrors!.Count);
            Assert.AreEqual("Invalid input", capturedErrors[0].Message);
        }

        #endregion

        #region Then Methods

        [Test]
        public void Then_ChainsSuccessResultsCorrectly()
        {
            var initialResult = Result<int>.Success(10);
            var finalResult = initialResult.Then(value => Result<string>.Success($"Value: {value}"));

            Assert.IsTrue(finalResult.IsSuccess);
            Assert.AreEqual("Value: 10", finalResult.Value);
        }

        [Test]
        public void Then_StopsOnFailure()
        {
            var error = new Error("Step 1 failed", ErrorCode.Failure);
            var initialResult = Result<int>.Failure(error);
            var finalResult = initialResult.Then(value => Result<string>.Success($"Value: {value}"));

            Assert.IsTrue(finalResult.IsFailure);
            Assert.AreEqual(1, finalResult.Errors.Count);
            Assert.AreEqual("Step 1 failed", finalResult.Errors.ElementAt(0).Message);
        }

        [Test]
        public void Then_WithGenericInput_WhenPreviousResultIsSuccess_ShouldReturnSuccess()
        {
            var initialResult = Result<int>.Success(10);
            var finalResult = initialResult.Then(value => Result<string>.Success($"Value: {value}"));

            Assert.IsTrue(finalResult.IsSuccess);
            Assert.AreEqual("Value: 10", finalResult.Value);
        }

        [Test]
        public void Then_WithGenericInput_WhenPreviousResultIsFailure_ShouldReturnFailure()
        {
            var error = new Error("Initial failure", ErrorCode.Failure);
            var initialResult = Result<int>.Failure(error);
            var finalResult = initialResult.Then(value => Result<string>.Success($"Value: {value}"));

            Assert.IsTrue(finalResult.IsFailure);
            Assert.AreEqual(1, finalResult.Errors.Count);
            Assert.AreEqual("Initial failure", finalResult.Errors.ElementAt(0).Message);
        }

        [Test]
        public void Then_StopsOnFailure_WithMeadianResult()
        {
            var error = new Error("Step 1 failed", ErrorCode.Failure);
            var initialResult = Result<int>.Failure(error);
            var finalResult = initialResult
                .Then(value => Result<int>.Success(value + 1))
                .Then(value => Result<string>.Success($"Value: {value}"));

            Assert.IsTrue(finalResult.IsFailure);
            Assert.AreEqual(1, finalResult.Errors.Count);
            Assert.AreEqual("Step 1 failed", finalResult.Errors.ElementAt(0).Message);
        }

        [Test]
        public void Then_WhenPreviousResultIsSuccess_ShouldReturnSuccess()
        {
            var initialResult = Result.Success();
            var finalResult = initialResult.Then(() => Result.Success());

            Assert.IsTrue(finalResult.IsSuccess);
        }

        [Test]
        public void Then_WhenPreviousResultIsFailure_ShouldReturnFailure()
        {
            var error = new Error("Initial failure");
            var initialResult = Result.Failure(error);
            var finalResult = initialResult.Then(() => Result.Success());

            Assert.IsTrue(finalResult.IsFailure);
            Assert.AreEqual(error, finalResult.Errors.ElementAt(0));
        }

        [Test]
        public void Thens_WhenPreviousResultIsSuccess_GenericShouldReturnSucces()
        {
            var initialResult = Result.Success();
            var finalResult = initialResult.Then(() => Result<int>.Success(42));

            Assert.IsTrue(finalResult.IsSuccess);
            Assert.AreEqual(42, finalResult.Value);
        }

        [Test]
        public void Then_WhenPreviousResultIsFailure_GenericShouldReturnFailure()
        {
            var error = new Error("Initial failure");
            var initialResult = Result.Failure(error);
            var finalResult = initialResult.Then(() => Result<int>.Success(42));

            Assert.IsTrue(finalResult.IsFailure);
            Assert.AreEqual(error, finalResult.Errors.ElementAt(0));
        }

        [Test]
        public void Then_WithGenericInput_NextFunctionWithoutInput_ShouldReturnSuccess()
        {
            var initialResult = Result<int>.Success(10);
            var finalResult = initialResult.Then(() => Result<string>.Success("Value"));

            Assert.IsTrue(finalResult.IsSuccess);
            Assert.AreEqual("Value", finalResult.Value);
        }

        [Test]
        public void Then_WithGenericInput_NextFunctionWithoutInput_ShouldReturnFailure()
        {
            var error = new Error("Initial failure", ErrorCode.Failure);
            var initialResult = Result<int>.Failure(error);
            var finalResult = initialResult.Then(() => Result<string>.Success("Value"));

            Assert.IsTrue(finalResult.IsFailure);
            Assert.AreEqual(1, finalResult.Errors.Count);
            Assert.AreEqual("Initial failure", finalResult.Errors.ElementAt(0).Message);
        }

        [Test]
        public void Then_WithGenericInput_WithoutGenericOutput_NextFunctoinWithoutInput_ShouldReturnSuccess()
        {
            var initialResult = Result<int>.Success(10);
            var finalResult = initialResult.Then(() => Result.Success());
            Assert.IsTrue(finalResult.IsSuccess);
        }

        [Test]
        public void Then_WithGenericInput_WithoutGenericOutput_NextFunctoinWithoutInput_ShouldReturnFailure()
        {
            var initialResult = Result<int>.Failure(Error.Failure());
            var finalResult = initialResult.Then(() => Result.Success());
            Assert.IsTrue(finalResult.IsFailure);
        }

        [Test]
        public void Then_WithGenericInput_WithoutGenericOutput_NextFunctoinWithGenericInput_ShouldReturnSuccess()
        {
            var initialResult = Result<int>.Success(10);
            var finalResult = initialResult.Then((value) => value == 10 ? Result.Success() : Result.Failure());
            Assert.IsTrue(finalResult.IsSuccess);
        }

        [Test]
        public void Then_WithGenericInput_WithoutGenericOutput_NextFunctoinWithGenericInput_ShouldReturnFailure()
        {
            var initialResult = Result<int>.Failure(Error.Failure());
            var finalResult = initialResult.Then((value) => Result.Success());
            Assert.IsTrue(finalResult.IsFailure);
        }

        #endregion

        #region Map Methods

        [Test]
        public void Map_TransformsSuccessfulResult()
        {
            var result = Result<int>.Success(5);
            var mappedResult = result.Map(x => x * 2);

            Assert.IsTrue(mappedResult.IsSuccess);
            Assert.AreEqual(10, mappedResult.Value);
        }

        [Test]
        public void Map_DoesNotTransformFailedResult()
        {
            var error = new Error("Mapping failed", ErrorCode.Failure);
            var result = Result<int>.Failure(error);
            var mappedResult = result.Map(x => x * 2);

            Assert.IsTrue(mappedResult.IsFailure);
            Assert.AreEqual(1, mappedResult.Errors.Count);
            Assert.AreEqual("Mapping failed", mappedResult.Errors.ElementAt(0).Message);
        }

        #endregion

        #region Ensure Methods

        [Test]
        public void Ensure_SuccessfulResultThatMeetsPredicate_ReturnsSameResult()
        {
            var result = Result<int>.Success(10);
            var ensuredResult = result.Ensure(x => x > 5, new Error("Value too small", ErrorCode.Validation));

            Assert.IsTrue(ensuredResult.IsSuccess);
            Assert.AreEqual(10, ensuredResult.Value);
        }

        [Test]
        public void Ensure_SuccessfulResultThatFailsPredicate_ReturnsFailure()
        {
            var result = Result<int>.Success(3);
            var ensuredResult = result.Ensure(x => x > 5, new Error("Value too small", ErrorCode.Validation));

            Assert.IsTrue(ensuredResult.IsFailure);
            Assert.AreEqual("Value too small", ensuredResult.Errors.ElementAt(0).Message);
        }

        #endregion

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
        public void Try_ExecutesSuccessfully_ReturnsGenericSuccess()
        {
            var result = Result.Try(() => { return 10; }, ex => new Error(ex.Message));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value, 10);
        }

        #endregion

        #region OnSuccess and OnFailure Methods

        [Test]
        public void OnSuccess_NonGeneric_Success_ExecutesAction()
        {
            var result = Result.Success();
            bool actionExecuted = false;
            result.OnSuccess(() => actionExecuted = true);
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public void OnSuccess_NonGeneric_Failure_DoesNotExecuteAction()
        {
            var error = new Error("Failure", ErrorCode.Failure);
            var result = Result.Failure(error);
            result.OnSuccess(() => Assert.Fail());
        }

        [Test]
        public void OnSuccess_Generic_Success_ExecutesAction()
        {
            var result = Result<int>.Success(30);
            int captured = 0;
            result.OnSuccess(val => captured = val);
            Assert.AreEqual(30, captured);
        }

        [Test]
        public void OnSuccess_Generic_Failure_DoesNotExecuteAction()
        {
            var error = new Error("Failure", ErrorCode.Failure);
            var result = Result<int>.Failure(error);
            result.OnSuccess(val => Assert.Fail());
        }

        [Test]
        public void OnFailure_NonGeneric_Failure_ExecutesAction()
        {
            var error = new Error("Failure", ErrorCode.Failure);
            var result = Result.Failure(error);
            bool actionExecuted = false;
            result.OnFailure(() => actionExecuted = true);
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public void OnFailure_NonGeneric_Success_DoesNotExecuteAction()
        {
            var result = Result.Success();
            result.OnFailure(() => Assert.Fail());
        }

        [Test]
        public void OnFailure_Generic_Failure_ExecutesAction()
        {
            var error = new Error("Failure", ErrorCode.Failure);
            var result = Result<int>.Failure(error);
            bool actionExecuted = false;
            result.OnFailure(val => actionExecuted = true);
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public void OnFailure_Generic_Success_DoesNotExecuteAction()
        {
            var result = Result<int>.Success(100);
            result.OnFailure(val => Assert.Fail());
        }

        #endregion

        #region Unwrap and UnwrapOrDefault

        [Test]
        public void UnwrapOrDefault_Success_ReturnsValue()
        {
            var result = Result<int>.Success(50);
            int value = result.UnwrapOrDefault(0);
            Assert.AreEqual(50, value);
        }

        [Test]
        public void UnwrapOrDefault_Failure_ReturnsDefault()
        {
            var error = new Error("Error", ErrorCode.Failure);
            var result = Result<int>.Failure(error);
            int value = result.UnwrapOrDefault(99);
            Assert.AreEqual(99, value);
        }

        [Test]
        public void Unwrap_Success_ReturnsValue()
        {
            var result = Result<int>.Success(60);
            int value = result.Unwrap();
            Assert.AreEqual(60, value);
        }

        [Test]
        public void Unwrap_Failure_ThrowsException()
        {
            var error = new Error("Unwrap error", ErrorCode.Failure);
            var result = Result<int>.Failure(error);
            var ex = Assert.Throws<InvalidOperationException>(() => result.Unwrap());
            Assert.IsTrue(ex.Message.Contains("Unwrap error"));
        }

        #endregion

        #region OrElse Methods

        [Test]
        public void OrElse_NonGeneric_Success_ReturnsOriginal()
        {
            var result = Result.Success();
            var alternative = Result.Failure(new Error("Alternative", ErrorCode.Failure));
            var final = result.OrElse(() => alternative);
            Assert.IsTrue(final.IsSuccess);
        }

        [Test]
        public void OrElse_NonGeneric_Failure_ReturnsAlternative()
        {
            var error = new Error("Original failure", ErrorCode.Failure);
            var result = Result.Failure(error);
            var alternative = Result.Success();
            var final = result.OrElse(() => alternative);
            Assert.IsTrue(final.IsSuccess);
        }

        [Test]
        public void OrElse_Generic_Success_ReturnsOriginal()
        {
            var result = Result<int>.Success(123);
            var alternative = Result<int>.Failure(new Error("Alternative", ErrorCode.Failure));
            var final = result.OrElse(() => alternative);
            Assert.IsTrue(final.IsSuccess);
            Assert.AreEqual(123, final.Value);
        }

        [Test]
        public void OrElse_Generic_Failure_ReturnsAlternative()
        {
            var error = new Error("Original failure", ErrorCode.Failure);
            var result = Result<int>.Failure(error);
            var alternative = Result<int>.Success(456);
            var final = result.OrElse(() => alternative);
            Assert.IsTrue(final.IsSuccess);
            Assert.AreEqual(456, final.Value);
        }

        #endregion
    }
}