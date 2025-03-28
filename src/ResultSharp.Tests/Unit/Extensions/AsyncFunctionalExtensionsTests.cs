using NUnit.Framework;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using ResultSharp.Extensions.FunctionalExtensions.Async;

namespace ResultSharp.Tests.Unit.Extensions
{
    [TestFixture]
    internal class AsyncFunctionalExtensionsTests
    {
        #region EnsureAsync Methods

        [Test]
        public async Task EnsureAsync_WithPredicate_WhenResultIsSuccessAndPredicateIsTrue_ShouldReturnSuccess()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.EnsureAsync(value => value > 5);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task EnsureAsync_WithPredicate_WhenResultIsSuccessAndPredicateIsFalse_ShouldReturnFailureWithDefaultError()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.EnsureAsync(value => value < 5);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Operation failure.", result.Errors.First().Message);
        }

        [Test]
        public async Task EnsureAsync_WithPredicate_WhenResultIsSuccessAndPredicateIsFalse_ShouldReturnFailureWithSpecifiedError()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));
            var customError = Error.Unauthorized("Custom error");

            // Act
            var result = await resultTask.EnsureAsync(value => value < 5, customError);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Custom error", result.Errors.First().Message);
        }

        [Test]
        public async Task EnsureAsync_WithPredicate_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.EnsureAsync(value => value > 5);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task EnsureAsync_WithAsyncPredicate_WhenResultIsSuccessAndPredicateIsTrue_ShouldReturnSuccess()
        {
            var resultTask = Task.FromResult(Result.Success(10));

            var result = await resultTask.EnsureAsync(async value =>
            {
                await Task.Delay(1);
                return value > 5;
            });

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task EnsureAsync_WithAsyncPredicate_WhenResultIsSuccessAndPredicateIsFalse_ShouldReturnFailure()
        {
            var resultTask = Task.FromResult(Result.Success(10));

            var result = await resultTask.EnsureAsync(async value =>
            {
                await Task.Delay(1);
                return value < 5;
            });

            Assert.IsTrue(result.IsFailure);
        }

        [Test]
        public async Task EnsureAsync_WithAsyncPredicate_WhenResultIsFailureAndPredicateIsFalse_ShouldReturnFailure()
        {
            var resultTask = Task.FromResult(Result<int>.Failure());

            var result = await resultTask.EnsureAsync(async value =>
            {
                await Task.Delay(1);
                return value < 5;
            });

            Assert.IsTrue(result.IsFailure);
        }

        [Test]
        public async Task EnsureAsync_WithAsyncPredicateAndSyncResult_WhenResultIsSuccessAndPredicateIsTrue_ShouldReturnSuccess()
        {
            var result = Result.Success(10);

            var actual = await result.EnsureAsync(async value =>
            {
                await Task.Delay(1);
                return value > 5;
            });

            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public async Task EnsureAsync_WithAsyncPredicateAndSyncResult_WhenResultIsSuccessAndPredicateIsFalse_ShouldReturnFailure()
        {
            var resultTask = Result.Success(10);

            var result = await resultTask.EnsureAsync(async value =>
            {
                await Task.Delay(1);
                return value < 5;
            });

            Assert.IsTrue(result.IsFailure);
        }

        [Test]
        public async Task EnsureAsync_WithAsyncPredicateAndSyncResult_WhenResultIsFailureAndPredicateIsFalse_ShouldReturnFailure()
        {
            var resultTask = Result<int>.Failure();

            var result = await resultTask.EnsureAsync(async value =>
            {
                await Task.Delay(1);
                return value < 5;
            });

            Assert.IsTrue(result.IsFailure);
        }

        #endregion

        #region MapAsync Methods

        [Test]
        public async Task MapAsync_WhenResultIsSuccess_ShouldReturnMappedSuccess()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.MapAsync(value => $"Value: {value}");

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Value: 10", result.Value);
        }

        [Test]
        public async Task MapAsync_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var error = new Error("Initial failure", ErrorCode.Failure);
            var resultTask = Task.FromResult(Result<int>.Failure(error));

            // Act
            var result = await resultTask.MapAsync(value => $"Value: {value}");

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("Initial failure", result.Errors.ElementAt(0).Message);
        }

        [Test]
        public async Task MapAsync_WithSuccessfulResultAndAsyncMap_ShouldReturnMappedResult()
        {
            // Arrange
            var result = Task.FromResult(Result.Success(10));

            // Act
            var mappedResult = await result.MapAsync(async value =>
            {
                await Task.Delay(10);
                return value.ToString();
            });

            // Assert
            Assert.IsTrue(mappedResult.IsSuccess);
            Assert.AreEqual("10", mappedResult.Value);
        }

        [Test]
        public async Task MapAsync_WithFailedResultAndAsyncMap_ShouldReturnFailedResult()
        {
            // Arrange
            var errors = new List<Error> { Error.Failure("Test error") };
            var result = Task.FromResult(Result<int>.Failure(errors));

            // Act
            var mappedResult = await result.MapAsync(async value =>
            {
                await Task.Delay(10);
                return value.ToString();
            });

            // Assert
            Assert.IsTrue(mappedResult.IsFailure);
            Assert.AreEqual(errors, mappedResult.Errors);
        }

        [Test]
        public async Task MapAsync_WithSuccessfulResultAndAsyncMapDirect_ShouldReturnMappedResult()
        {
            // Arrange
            var result = Result.Success(10);

            // Act
            var mappedResult = await result.MapAsync(async value =>
            {
                await Task.Delay(10);
                return value.ToString();
            });

            // Assert
            Assert.IsTrue(mappedResult.IsSuccess);
            Assert.AreEqual("10", mappedResult.Value);
        }

        [Test]
        public async Task MapAsync_WithFailedResultAndAsyncMapDirect_ShouldReturnFailedResult()
        {
            // Arrange
            var errors = new List<Error> { Error.Failure("Test error") };
            var result = Result<int>.Failure(errors);

            // Act
            var mappedResult = await result.MapAsync(async value =>
            {
                await Task.Delay(10);
                return value.ToString();
            });

            // Assert
            Assert.IsTrue(mappedResult.IsFailure);
            Assert.AreEqual(errors, mappedResult.Errors);
        }

        #endregion

        #region MatchAsync Methods

        [Test]
        public async Task MatchAsync_WithSuccessResult_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Success());
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: () => onSuccessExecuted = true,
                onFailure: _ => onFailureExecuted = false
            );

            // Assert
            Assert.IsTrue(onSuccessExecuted);
            Assert.IsFalse(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithFailureResult_ShouldExecuteOnFailureAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Failure(Error.Failure("Test error")));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: () => onSuccessExecuted = false,
                onFailure: _ => onFailureExecuted = true
            );

            // Assert
            Assert.IsFalse(onSuccessExecuted);
            Assert.IsTrue(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithSuccessResultAndAsyncActions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Success());
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async () =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = true;
                },
                onFailure: async _ =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = false;
                }
            );

            // Assert
            Assert.IsTrue(onSuccessExecuted);
            Assert.IsFalse(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithFailureResultAndAsyncActions_ShouldExecuteOnFailureAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Failure(Error.Failure("Test error")));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async () =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = false;
                },
                onFailure: async _ =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = true;
                }
            );

            // Assert
            Assert.IsFalse(onSuccessExecuted);
            Assert.IsTrue(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithSuccessResultDirect_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var result = Result.Success();
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async () =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = true;
                },
                onFailure: async _ =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = false;
                }
            );

            // Assert
            Assert.IsTrue(onSuccessExecuted);
            Assert.IsFalse(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithFailureResultDirect_ShouldExecuteOnFailureAction()
        {
            // Arrange
            var result = Result.Failure(Error.Failure("Test error"));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async () =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = false;
                },
                onFailure: async _ =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = true;
                }
            );

            // Assert
            Assert.IsFalse(onSuccessExecuted);
            Assert.IsTrue(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithGenericSuccessResult_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Success(10));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: value => onSuccessExecuted = value == 10,
                onFailure: errors => onFailureExecuted = false
            );

            // Assert
            Assert.IsTrue(onSuccessExecuted);
            Assert.IsFalse(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithGenericFailureResult_ShouldExecuteOnFailureAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Failure(Error.Failure("Test error")));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: value => onSuccessExecuted = false,
                onFailure: errors => onFailureExecuted = true
            );

            // Assert
            Assert.IsFalse(onSuccessExecuted);
            Assert.IsTrue(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithGenericSuccessResultAndAsyncActions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Success(10));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async value =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = value == 10;
                },
                onFailure: async errors =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = false;
                }
            );

            // Assert
            Assert.IsTrue(onSuccessExecuted);
            Assert.IsFalse(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithGenericFailureResultAndAsyncActions_ShouldExecuteOnFailureAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Failure(Error.Failure("Test error")));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async value =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = false;
                },
                onFailure: async errors =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = true;
                }
            );

            // Assert
            Assert.IsFalse(onSuccessExecuted);
            Assert.IsTrue(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithGenericSuccessResultDirect_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var result = Result<int>.Success(10);
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async value =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = value == 10;
                },
                onFailure: async errors =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = false;
                }
            );

            // Assert
            Assert.IsTrue(onSuccessExecuted);
            Assert.IsFalse(onFailureExecuted);
        }

        [Test]
        public async Task MatchAsync_WithGenericFailureResultDirect_ShouldExecuteOnFailureAction()
        {
            // Arrange
            var result = Result<int>.Failure(Error.Failure("Test error"));
            var onSuccessExecuted = false;
            var onFailureExecuted = false;

            // Act
            await result.MatchAsync(
                onSuccess: async value =>
                {
                    await Task.Delay(10);
                    onSuccessExecuted = false;
                },
                onFailure: async errors =>
                {
                    await Task.Delay(10);
                    onFailureExecuted = true;
                }
            );

            // Assert
            Assert.IsFalse(onSuccessExecuted);
            Assert.IsTrue(onFailureExecuted);
        }

        #endregion

        #region OnSuccessAsync / OnFailureAsync Methods

        #region OnSuccessAsync Methods

        [Test]
        public async Task OnSuccessAsync_WithSuccessResult_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Success());
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(() => actionExecuted = true);

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithFailureResult_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(() => actionExecuted = true);

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithSuccessResultAndAsyncAction_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Success());
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithFailureResultAndAsyncAction_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithSuccessResultDirect_ShouldExecuteAction()
        {
            // Arrange
            var result = Result.Success();
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithFailureResultDirect_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Result.Failure(Error.Failure("Test error"));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericSuccessResult_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Success(10));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(value => actionExecuted = value == 10);

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericFailureResult_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(value => actionExecuted = true);

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericSuccessResultAndAsyncAction_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Success(10));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async value =>
            {
                await Task.Delay(10);
                actionExecuted = value == 10;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericFailureResultAndAsyncAction_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async value =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericSuccessResultDirect_ShouldExecuteAction()
        {
            // Arrange
            var result = Result<int>.Success(10);
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async value =>
            {
                await Task.Delay(10);
                actionExecuted = value == 10;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericFailureResultDirect_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Result<int>.Failure(Error.Failure("Test error"));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async value =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericFailureResultDirectAndAsyncAction_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Result<int>.Failure(Error.Failure("Test error"));
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnSuccessAsync_WithGenericSuccessResultDirectAndAsyncAction_ShouldExecuteAction()
        {
            // Arrange
            var result = Result<int>.Success(10);
            var actionExecuted = false;

            // Act
            await result.OnSuccessAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        #endregion

        #region OnFailureAsync Methods

        [Test]
        public async Task OnFailureAsync_WithFailureResultAndErrorHandleFunc_ShouldExecuteAction()
        {
            var result = Task.FromResult(Result.Failure(Error.Failure()));
            var actionExecuted = false;

            await result.OnFailureAsync(async errors => actionExecuted = errors.Any());

            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithSuccessResultAndErrorHandleFunc_ShouldNotExecuteAction()
        {
            var result = Task.FromResult(Result.Success());
            var actionExecuted = false;

            await result.OnFailureAsync(async errors => actionExecuted = errors.Any());

            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithFailureResult_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(() => actionExecuted = true);

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithSuccessResult_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Success());
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(() => actionExecuted = true);

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithFailureResultAndAsyncAction_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithSuccessResultAndAsyncAction_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result.Success());
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithFailureResultDirect_ShouldExecuteAction()
        {
            // Arrange
            var result = Result.Failure(Error.Failure("Test error"));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithSuccessResultDirect_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Result.Success();
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericFailureResult_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(errors => actionExecuted = true);

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericSuccessResult_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Success(10));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(errors => actionExecuted = true);

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericFailureResultAndAsyncAction_ShouldExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Failure(Error.Failure("Test error")));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async errors =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericSuccessResultAndAsyncAction_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Task.FromResult(Result<int>.Success(10));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async errors =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericFailureResultDirect_ShouldExecuteAction()
        {
            // Arrange
            var result = Result<int>.Failure(Error.Failure("Test error"));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async errors =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericSuccessResultDirect_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Result<int>.Success(10);
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async errors =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericFailureResultDirectAndAsyncAction_ShouldExecuteAction()
        {
            // Arrange
            var result = Result<int>.Failure(Error.Failure("Test error"));
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [Test]
        public async Task OnFailureAsync_WithGenericSuccessResultDirectAndAsyncAction_ShouldNotExecuteAction()
        {
            // Arrange
            var result = Result<int>.Success(10);
            var actionExecuted = false;

            // Act
            await result.OnFailureAsync(async () =>
            {
                await Task.Delay(10);
                actionExecuted = true;
            });

            // Assert
            Assert.IsFalse(actionExecuted);
        }

        #endregion

        #endregion

        #region OrElseAsync Methods

        [Test]
        public async Task OrElse_WhenResultIsSuccess_ShouldReturnOriginalResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Success());

            // Act
            var result = await resultTask.OrElseAsync(() => Result.Failure(Error.Failure("Alternative failure")));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task OrElse_WhenResultIsFailure_ShouldReturnAlternativeResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.OrElseAsync(() => Result.Success());

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task OrElseAsync_WhenResultIsSuccess_ShouldReturnOriginalResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Success());

            // Act
            var result = await resultTask.OrElseAsync(() => Task.FromResult(Result.Failure(Error.Failure("Alternative failure"))));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task OrElseAsync_WhenResultIsFailure_ShouldReturnAlternativeResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.OrElseAsync(() => Task.FromResult(Result.Success()));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task OrElse_WithGenericResult_WhenResultIsSuccess_ShouldReturnOriginalResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.OrElseAsync(() => Result<int>.Failure(Error.Failure("Alternative failure")));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task OrElse_WithGenericResult_WhenResultIsFailure_ShouldReturnAlternativeResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.OrElseAsync(() => Result<int>.Success(20));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(20, result.Value);
        }

        [Test]
        public async Task OrElseAsync_WithGenericResult_WhenResultIsSuccess_ShouldReturnOriginalResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.OrElseAsync(() => Task.FromResult(Result<int>.Failure(Error.Failure("Alternative failure"))));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task OrElseAsync_WithGenericResult_WhenResultIsFailure_ShouldReturnAlternativeResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.OrElseAsync(() => Task.FromResult(Result<int>.Success(20)));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(20, result.Value);
        }

        #endregion

        #region ThenAsync Methods

        [Test]
        public async Task ThenAsync_WithTaskResult_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Success());

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result.Success()));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task ThenAsync_WithTaskResult_WhenResultIsFailure_ShouldReturnOriginalResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result.Success()));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithResult_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Success());

            // Act
            var result = await resultTask.ThenAsync(() => Result.Success());

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task ThenAsync_WithResult_WhenResultIsFailure_ShouldReturnOriginalResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Result.Success());

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericTaskResult_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Success());

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result<int>.Success(10)));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task ThenAsync_WithGenericTaskResult_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result<int>.Success(10)));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericResult_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Success());

            // Act
            var result = await resultTask.ThenAsync(() => Result<int>.Success(10));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task ThenAsync_WithGenericResult_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Result<int>.Success(10));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputTaskResult_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.ThenAsync(value => Task.FromResult(Result<string>.Success($"Value: {value}")));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Value: 10", result.Value);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputTaskResult_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(value => Task.FromResult(Result<string>.Success($"Value: {value}")));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResult_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.ThenAsync(value => Result<string>.Success($"Value: {value}"));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Value: 10", result.Value);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResult_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(value => Result<string>.Success($"Value: {value}"));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputTaskResultAndNoInput_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result<string>.Success("Success")));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Success", result.Value);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputTaskResultAndNoInput_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result<string>.Success("Success")));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoInput_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.ThenAsync(() => Result<string>.Success("Success"));

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Success", result.Value);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoInput_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Result<string>.Success("Success"));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputTaskResultAndNoOutput_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result.Success()));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputTaskResultAndNoOutput_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Task.FromResult(Result.Success()));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoOutput_WhenResultIsSuccess_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.ThenAsync(() => Result.Success());

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoOutput_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync(() => Result.Success());

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoOutput_WhenResultIsFailureAndNextFuncWithGenericInput_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync((val) => Result.Success());

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoOutput_WhenResultIsSuccessAndNextFuncWithGenericInput_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(5));

            // Act
            var result = await resultTask.ThenAsync((val) => Result.Success());

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoOutput_WhenResultIsFailureAndNextFuncWithGenericTaskInput_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.ThenAsync((val) => Task.FromResult(Result.Success()));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public async Task ThenAsync_WithGenericInputResultAndNoOutput_WhenResultIsSuccessAndNextFuncWithGenericTaskInput_ShouldReturnNextResult()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(5));

            // Act
            var result = await resultTask.ThenAsync((val) => Task.FromResult(Result.Success()));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region UnwrapAsync Methods

        [Test]
        public async Task UnwrapOrDefaultAsync_WhenResultIsSuccess_ShouldReturnResultValue()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.UnwrapOrDefaultAsync(0);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public async Task UnwrapOrDefaultAsync_WhenResultIsFailure_ShouldReturnDefaultValue()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = await resultTask.UnwrapOrDefaultAsync(0);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task UnwrapAsync_WhenResultIsSuccess_ShouldReturnResultValue()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = await resultTask.UnwrapAsync();

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void UnwrapAsync_WhenResultIsFailure_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await resultTask.UnwrapAsync());
            Assert.AreEqual("Initial failure", ex?.Message);
        }

        #endregion

        #region AwaitResult Methods

        [Test]
        public void Wait_WhenResultIsSuccess_ShouldReturnSuccess()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Success());

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Wait_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result.Failure(Error.Failure("Initial failure")));

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public void Wait_WhenTaskIsCanceled_ShouldReturnTaskCanceledError()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            var resultTask = Task.Run(() =>
            {
                cts.Cancel();
                cts.Token.ThrowIfCancellationRequested();
                return Result.Success();
            }, cts.Token);

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("The operation was canceled.", result.Errors.First().Message);
        }

        [Test]
        public void Wait_WhenTaskThrowsException_ShouldReturnFailureWithExceptionMessage()
        {
            // Arrange
            var resultTask = Task.Run(() => ThrowAsync());

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Test exception", result.Errors.First().Message);
        }

        private Task<Result<int>> ThrowAsync()
        {
            throw new InvalidOperationException("Test exception");
        }

        [Test]
        public void Wait_WithGenericResult_WhenResultIsSuccess_ShouldReturnSuccess()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Success(10));

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public void Wait_WithGenericResult_WhenResultIsFailure_ShouldReturnFailure()
        {
            // Arrange
            var resultTask = Task.FromResult(Result<int>.Failure(Error.Failure("Initial failure")));

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Initial failure", result.Errors.First().Message);
        }

        [Test]
        public void Wait_WithGenericResult_WhenTaskIsCanceled_ShouldReturnTaskCanceledError()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            var resultTask = Task.Run(() =>
            {
                cts.Cancel();
                cts.Token.ThrowIfCancellationRequested();
                return Result<int>.Success(10);
            }, cts.Token);

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("The operation was canceled.", result.Errors.First().Message);
        }

        [Test]
        public void Wait_WithGenericResult_WhenTaskThrowsException_ShouldReturnFailureWithExceptionMessage()
        {
            // Arrange
            var resultTask = Task.Run(() => ThrowGenericAsync());

            // Act
            var result = resultTask.AwaitResult();

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Test exception", result.Errors.First().Message);
        }

        private Task<Result<int>> ThrowGenericAsync()
        {
            throw new InvalidOperationException("Test exception");
        }

        #endregion

    }
}