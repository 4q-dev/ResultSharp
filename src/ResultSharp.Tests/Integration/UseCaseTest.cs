using NUnit.Framework;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;
using ResultSharp.Extensions.CollectionExtensions;
using ResultSharp.Extensions.FunctionalExtensions.Async;
using ResultSharp.Extensions.FunctionalExtensions.Sync;
using ResultSharp.Logging;

namespace ResultSharp.Tests.Integration
{
    [TestFixture]
    internal class UseCaseTest
    {
        private readonly Repository userRepository = new();
        private readonly NotificationService emailNotificationService = new NotificationService();

        [Test]
        public void Test()
        {
            /*
            // Без Result Pattern'а
            var user = userRepository.Get();
            if (user is null)
            {
                logger.LogMessage("User not found");
                throw new Exception("User not found");
            }

            if (user.Email.IsConfirmed is false)
            {
                logger.LogMessage("Email address must be confirmed before sending notifications.");
                throw new Exception("Email address must be confirmed before sending notifications.");
            }

            try
            {
                emailNotificationService.Notify(user.Email, "some notification message");
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Error message: {ex}", ex.Message);
                throw ex;
            }
            */

            var actual = userRepository.Get()
                .Ensure(user => user.Email.IsConfirmed, onFailure: Error.Unauthorized("Email address must be confirmed before sending notifications."))
                .Then(user => emailNotificationService.Notify(user.Email, "some notification message"));

            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void Test2()
        {
            int result = ParseNumber("42")
                .Map(n => n * 2)
                .Match(
                    ok =>
                    {
                        Console.WriteLine($"Number is {ok}");
                        return Result.Success(ok + 10);
                    },
                    errs =>
                    {
                        Console.WriteLine($"Errors: {errs.SummaryErrorMessages()}");
                        return Error.Failure("Some failure message");
                    }
                )
                //.LogIfSuccess("Log value: {val}") // output: Log value: 94
                .UnwrapOrDefault(@default: 0);

            Console.WriteLine(result); // 94
            Assert.That(result, Is.EqualTo(94));
        }

        private Result<int> ParseNumber(string input)
        {
            return int.TryParse(input, out var number)
                ? number
                : Error.Failure("Invalid number");
        }

        [Test]
        public async Task MatchDocumentationCase()
        {
            Task<Result<string>> result = Task.FromResult(Result.Success(""));
            Result result2 = Result.Success();

            var matchResult = await result.MatchAsync(
                val => {
                    if (val == "Привет от LightChimera!")
                        return "удачи поклинкодить!";
                    return val;
                },
                errs => Result<string>.Failure(),
                configureAwait: false
            );

            var matchResult2 = result2.Match(
                onSuccess: () => {
                    // some operations here
                    return Result.Success(10);
                },
                onFailure: errors => {
                    // the logic of handling some errors is as follows
                    if (errors.First().ErrorCode == ErrorCode.NotFound)
                        return Result.Success(-1);

                    return Result<int>.Failure();
                }
            );

            var matchResult3 = result.MatchAsync(
                ok => Result.Success(),
                err => Result.Failure()
            );
        }
    }

    internal class User
    {
        public Email Email { get; } = new();
    }

    internal class Email
    {
        public string Value => "some@mail.xyz";
        public bool IsConfirmed => true;

        public static implicit operator string(Email email)
            => email.Value;
    }

    internal class Repository
    {
        public Result<User> Get()
            => new User(); // if user not found: Error.NotFound("User not found");
    }

    internal class NotificationService
    {
        public Result Notify(Email email, string message)
        {
            Console.WriteLine($"Отправляю {message} на почту {email}");

            //if (someBadCondition)
            //    return Error.Failure("some failure message.");

            return Result.Success();
        }
    }
}
