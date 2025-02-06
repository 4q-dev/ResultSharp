using NUnit.Framework;
using ResultSharp.Errors;
using ResultSharp.Extensions;
using ResultSharp.Logging;

namespace ResultSharp.Tests.Extensions
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
                .Then(user => emailNotificationService.Notify(user.Email, "some notification message"))
                .LogIfFailure();

            Assert.IsTrue(actual.IsSuccess);
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
