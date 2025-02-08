<a id="readme-top"></a>
<br />
<div align="center">
  <a href="https://github.com/4q-dev/ResultSharp">
    <img src="images/logo2.jpg" alt="Logo" width="120" height="120">
  </a>

  # ResultSharp ![GitHub Repo stars](https://img.shields.io/github/stars/4q-dev/ResultSharp)
  
  <p align="center">
    A functional-style library for working with the Result pattern. Simplifies handling success and failure results without using exceptions, improving code readability and reliability.
    <br />
    <a href="https://resultsharp.lcma.tech"><strong>View Documentation »</strong></a>
    <br />
    <br />
    <a href="https://github.com/4q-dev/ResultSharp/blob/master/README.md"><strong>Russian version »</strong></a>
  </p>

  ![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/4q-dev/ResultSharp/cicd.yml?label=CI%2FCD)
  ![GitHub Release](https://img.shields.io/github/v/release/4q-dev/ResultSharp)
  ![GitHub last commit](https://img.shields.io/github/last-commit/4q-dev/ResultSharp)
  ![GitHub issues](https://img.shields.io/github/issues/4q-dev/ResultSharp)
  ![GitHub Pull Requests](https://img.shields.io/github/issues-pr/4q-dev/ResultSharp)
  ![GitHub License](https://img.shields.io/github/license/4q-dev/ResultSharp)
  ![Static Badge](https://img.shields.io/badge/By-lcma-purple)

  
  <br />
  <br />
  
  **[<kbd> <br> Report Bug <br> </kbd>](https://github.com/4q-dev/ResultSharp/issues)**
  **[<kbd> <br> Request Feature <br> </kbd>](https://github.com/4q-dev/ResultSharp/issues)**

  ![](https://count.getloli.com/get/@4q-dev.ResultSharp)

  ---
</div>

## Documentation
Full documentation is available here: [ResultSharp Docs](https://resultsharp.lcma.tech)

## Features
- Convenient representation of successful (`Success`) and unsuccessful (`Failure`) results
- Composition and transformation of results using functional methods (`Map`, `Then`, `Match`, etc.)
- Eliminating the need for `try-catch` in business logic

## Quick Start
```csharp
using ResultSharp;

Result<int> ParseNumber(string input)
{
    return int.TryParse(input, out var number)
        ? number
        : Error.Failure("Invalid number");
}

int result = ParseNumber("42")
    .Map(n => n * 2)
    .Match(
        ok => $"Success: {ok}",
        error => $"Error: {error}"
    );

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
