using NUnit.Framework;
using ResultSharp.Errors;
using ResultSharp.Extensions.FunctionalExtensions.Sync;

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
                    ok => Console.Write($"Success: {ok}"), // output: Success: 84
                    error => Console.Write($"Error: {error}")
                )
                .UnwrapOrDefault(@default: 0);

            Console.WriteLine(result); // 84
            Assert.That(result, Is.EqualTo(84));
        }

        private Result<int> ParseNumber(string input)
        {
            return int.TryParse(input, out var number)
                ? number
                : Error.Failure("Invalid number");
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
