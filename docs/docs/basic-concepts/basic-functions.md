---
title: Базовые функции для работы с результатами
description: Функции Try и Merge для работы с Result Pattern
---

# Базовые функции для работы с результатами

В рамках работы с Result Pattern существует несколько базовых функций, которые упрощают обработку результатов и ошибок:

- `Merge` — позволяет объединить результаты выполнения нескольких функций в один общий результат.
- `Try` — предоставляет безопасный способ выполнения кода, который может выбрасывать исключения, и автоматически оборачивает их в результат.

## Комбинирование результатов с помощью `Merge`

Функция `Merge` позволяет объединить несколько результатов типа `Result<T>` или `Result` в один. Это особенно полезно, когда необходимо проверить несколько условий или выполнить несколько валидаций, а затем обработать все ошибки в одном месте.

Пример использования:

```csharp
var nameResult = ValidateName(name);
var emailResult = ValidateEmail(email);

var validationResult = Result<string>.Merge(nameResult, emailResult);

if (validationResult.IsFailure)
{
    foreach (var error in validationResult.Errors)
        Console.WriteLine($"Validation failed: {error.Message}");
}
```

Так же поддерживается расширение для коллекций:

```csharp
var resutls = new[] { Result<int>.Success(5), Result<int>.Failure(Error.Failure()) };

var validationResult = results.Merge();

if (validationResult.IsFailure)
    Console.WriteLine(validationResult.SummaryErrorMessages());
```

> [!NOTE]
> Результатом объединения `Result<T>` будет объект, содержащий `ReadOnlyCollection<T>`.
> Например, при объединении двух результатов со значениями `"Foo"` и `"Bar"`, мы получим результат, содержащий коллекцию `["Foo", "Bar"]`

## Обработка исключений с помощью `Try`

Функция `Try` позволяет безопасно выполнять код, который может выбрасывать исключения, и автоматически преобразовывать их в результат. Это избавляет от необходимости явно использовать блоки `try-catch` и упрощает обработку ошибок.

Пример использования:

```csharp
var result = Result.Try(() => File.ReadAllText("config.json"), ex => Error.Failure(ex.Message));

if (result.IsFailure)
    Console.WriteLine($"Failed to read config: {result.Errors.First().Message}");
```