<a id="readme-top"></a>
<br />
<div align="center">
  <a href="https://github.com/4q-dev/ResultSharp">
    <img src="images/logo2.jpg" alt="Logo" width="120" height="120">
  </a>

  # ResultSharp ![GitHub Repo stars](https://img.shields.io/github/stars/4q-dev/ResultSharp)
  
  <p align="center">
    Библиотека в функциональном стиле для работы с паттерном Result. Упрощает обработку успешных и неуспешных результатов без использования исключений, улучшая читаемость и надежность кода.
    <br />
    <a href="https://resultsharp.lcma.tech"><strong>Ознакомиться с документацией »</strong></a>
    <br />
    <br />
    <a href="https://github.com/4q-dev/ResultSharp/blob/master/README_en.md"><strong>English version »</strong></a>
  </p>

  [![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/4q-dev/ResultSharp/deploy.yml?label=CI%2FCD)](https://github.com/4q-dev/ResultSharp/deployments)
  [![NuGet Version](https://img.shields.io/nuget/vpre/4q-dev.ResultSharp)](https://www.nuget.org/packages/4q-dev.ResultSharp/)
  [![GitHub last commit](https://img.shields.io/github/last-commit/4q-dev/ResultSharp)](https://github.com/4q-dev/ResultSharp/commit/dev)
  [![GitHub issues](https://img.shields.io/github/issues/4q-dev/ResultSharp)](https://github.com/4q-dev/ResultSharp/issues)
  [![GitHub Pull Requests](https://img.shields.io/github/issues-pr/4q-dev/ResultSharp)](https://github.com/4q-dev/ResultSharp/pulls)
  [![GitHub License](https://img.shields.io/github/license/4q-dev/ResultSharp)](https://github.com/4q-dev/ResultSharp/blob/dev/LICENSE)
  [![Static Badge](https://img.shields.io/badge/Light-Chimera-purple)](https://github.com/4q-dev/ResultSharp)
  
  <br />
  <br />
  
  **[<kbd> <br> Report Bug <br> </kbd>](https://github.com/4q-dev/ResultSharp/issues)**
  **[<kbd> <br> Request Feature <br> </kbd>](https://github.com/4q-dev/ResultSharp/issues)**

  ![](https://count.getloli.com/get/@4q-dev.ResultSharp)

  ---
</div>

## Документация

Полная документация доступна по ссылке: [ResultSharp Docs](https://resultsharp.lcma.tech)

## Возможности

- Удобное представление успешных (`Success`) и неуспешных (`Failure`) результатов
- Композиция и трансформация результатов с помощью функциональных методов (`Map`, `Then`, `Match` и др.)
- Исключение необходимости использовать `try-catch` в бизнес-логике
- Поддержка асинхронных операций
- Поддержка логгирования: Microsoft.Extensions.Logging, Serilog или любой другой кастомный адаптер

## Быстрый старт

### Установка

```shel
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

## Контрибуция

Мы приветствуем вклад в развитие библиотеки! Чтобы внести изменения:

1. Форкните репозиторий
2. Создайте новую ветку (`git checkout -b feature-branch`)
3. Внесите изменения и закоммитьте их (`git commit -m 'Добавлена новая функция'`)
4. Отправьте изменения (`git push origin feature-branch`)
5. Откройте Pull Request

## Лицензия

Проект распространяется под лицензией MIT. См. файл [LICENSE](https://github.com/4q-dev/ResultSharp/blob/main/LICENSE) для деталей.
