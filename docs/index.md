---
title: ResultSharp
description: Библиотека ResultSharp - реализация Result паттерна с функциональными расширениями и логированием
---

## Описание

Библиотека в функциональном стиле для работы с паттерном Result. Упрощает обработку успешных и неуспешных результатов без использования исключений, улучшая читаемость и надежность кода.

## Возможности

- Удобное представление успешных (`Success`) и неуспешных (`Failure`) результатов
- Композиция и трансформация результатов с помощью функциональных методов (`Map`, `Then`, `Match` и др.)
- Исключение необходимости использовать `try-catch` в бизнес-логике
- Поддержка асинхронных операций
- Поддержка логгирования: Microsoft.Extensions.Logging, Serilog или любой другой кастомный адаптер

## Быстрый старт

### Установка

```sh
dotnet add package 4q-dev.ResultSharp
```

### Базовое использование

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

## Пример использования

Без использования `Result`:

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

С использованием `ResultSharp`:

```csharp
return userRepository.Get()
    .Ensure(user => user.Email.IsConfirmed, onFailure: Error.Unauthorized("Email address must be confirmed before sending notifications."))
    .Then(user => emailNotificationService.Notify(user.Email, "some notification message"))
    .LogIfFailure();
```

## Репозиторий и лицензия

Весь исходный код открыт и доступен по ссылке в репозитории на [GitHub](https://github.com/4q-dev/ResultSharp).

Проект распространяется под лицензией MIT. См. файл [LICENSE](https://github.com/4q-dev/ResultSharp/blob/main/LICENSE) для деталей.
