
# ResultSharp

A functional-style library for working with the Result pattern. Simplifies handling success and failure results without using exceptions, improving code readability and reliability.

## Link to repository
[ResultSharp](https://github.com/4q-dev/ResultSharp)

## Documentation
Full documentation is available here: [ResultSharp Docs](https://resultsharp.lcma.tech)

## Features

- Convenient representation of successful (`Success`) and failed (`Failure`) results  
- Composition and transformation of results using functional methods (`Map`, `Then`, `Match`, etc.)  
- Eliminates the need to use `try-catch` in business logic  
- Support for asynchronous operations  
- Support for logging: Microsoft.Extensions.Logging, Serilog, or any other custom adapter  

## Quick Start

### Installation

```shel
dotnet add package 4q-dev.ResultSharp
```

### Basic usage

```csharp
using ResultSharp;
using ResultSharp.Errors;
using ResultSharp.Extensions.FunctionalExtensions.Sync;

Result<int> ParseNumber(string input)
{
    return int.TryParse(input, out var number)
        ? number
        : Error.Failure("Invalid number");
}

int result = ParseNumber("42")
    .Map(n => n * 2)
    .Match(
        ok => Console.Write($"Success: {ok}"), // output: Success: 84
        error => Console.Write($"Error: {error}")
    )
    .UnwrapOrDefault(@default: 0);

Console.WriteLine(result); // 84
```

## Example Usage
Without using `Result`:
```csharp
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
```
Using `ResultSharp`:
```csharp
return userRepository.Get()
    .Ensure(user => user.Email.IsConfirmed, onFailure: Error.Unauthorized("Email address must be confirmed before sending notifications."))
    .Then(user => emailNotificationService.Notify(user.Email, "some notification message"))
    .LogIfFailure();
```

## Contribution
We welcome contributions to the development of this library! To make changes:
1. Fork the repository
2. Create a new branch (`git checkout -b feature-branch`)
3. Make changes and commit them (`git commit -m 'Added new feature'`)
4. Push the changes (`git push origin feature-branch`)
5. Open a Pull Request

## License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/4q-dev/ResultSharp/blob/main/LICENSE) file for details.