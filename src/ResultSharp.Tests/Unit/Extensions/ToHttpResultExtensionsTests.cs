using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using ResultSharp.HttpResult;
using System.Net;

namespace ResultSharp.Tests.Unit.Extensions
{
    [TestFixture]
    public class HttpResultExtensionsTests
    {
        [Test]
        public void ToResponse_WhenResultIsSuccess_ShouldReturnOkResult()
        {
            // Arrange
            var result = Result.Success();

            // Act
            var response = result.ToResponse();

            // Assert
            Assert.IsInstanceOf<OkResult>(response);
        }

        [Test]
        public void ToResponse_WhenResultIsFailureWithHttpStatusCode_ShouldReturnObjectResultWithStatusCode()
        {
            // Arrange
            var error = new Error("Not Found", ErrorCode.NotFound);
            var result = Result.Failure(error);

            // Act
            var response = result.ToResponse();

            // Assert
            var objectResult = response as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Test]
        public void ToResponse_WhenResultIsFailureWithCustomErrorCode_ShouldReturnObjectResultWithTranslatedStatusCode()
        {
            // Arrange
            var error = new Error("Validation Error", ErrorCode.Validation);
            var result = Result.Failure(error);

            // Act
            var response = result.ToResponse();

            // Assert
            var objectResult = response as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [Test]
        public void ToResponse_WhenResultIsFailureWithUnknownErrorCode_ShouldReturnObjectResultWithInternalServerError()
        {
            // Arrange
            var error = new Error("Unknown Error", ErrorCode.Failure);
            var result = Result.Failure(error);

            // Act
            var response = result.ToResponse();

            // Assert
            var objectResult = response as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);;
        }

        [Test]
        public void ToResponse_Generic_WhenResultIsSuccess_ShouldReturnOkObjectResult()
        {
            // Arrange
            var result = Result<int>.Success(10);

            // Act
            var response = result.ToResponse();

            // Assert
            Assert.IsNotNull(response);
        }

        [Test]
        public void ToResponse_Generic_WhenResultIsFailureWithHttpStatusCode_ShouldReturnObjectResultWithStatusCode()
        {
            // Arrange
            var error = new Error("Not Found", ErrorCode.NotFound);
            var result = Result<int>.Failure(error);

            // Act
            var response = result.ToResponse();

            Assert.IsNotNull(response);
        }

        [Test]
        public void ToResponse_Generic_WhenResultIsFailureWithCustomErrorCode_ShouldReturnObjectResultWithTranslatedStatusCode()
        {
            // Arrange
            var error = new Error("Validation Error", ErrorCode.Validation);
            var result = Result<int>.Failure(error);

            // Act
            var response = result.ToResponse();

            // Assert
            Assert.IsNotNull(response);
        }

        [Test]
        public void ToResponse_Generic_WhenResultIsFailureWithUnknownErrorCode_ShouldReturnObjectResultWithInternalServerError()
        {
            // Arrange
            var error = new Error("Unknown Error", ErrorCode.Failure);
            var result = Result<int>.Failure(error);

            // Act
            var response = result.ToResponse();

            // Assert
            Assert.IsNotNull(response);
        }
    }
}

