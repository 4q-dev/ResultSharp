---
title: OrElse и OrElseAsync
description: Использование альтернативных результатов при неудачном исходе операции.
---

# OrElse

Методы `OrElse` и `OrElseAsync` позволяют задать альтернативное поведение, если операция завершилась неудачно.

## Когда использовать

- Если необходимо подставить альтернативный результат при неудачном исходе операции.
- При реализации резервных сценариев, например, получение данных из кеша, если основной запрос не удался.
- Для организации цепочек обработки результатов.

## Как это работает

- `OrElse` используется для синхронных операций.
- `OrElseAsync` обрабатывает результаты асинхронно.
- Если исходный `Result` успешен, возвращается он же.
- Если `Result` содержит ошибку, вызывается альтернативная функция.

## Пример использования

### Синхронный вариант

```csharp
Result<int> GetPrimaryData() => Result.Failure<int>(Error.Failure("Ошибка получения данных"));

Result<int> GetFallbackData() => Result.Success(42);

var result = GetPrimaryData().OrElse(GetFallbackData);
Console.WriteLine(result.Value); // 42
```

### Асинхронный вариант

```csharp
async Task<Result<int>> GetPrimaryDataAsync() => Result.Failure<int>(Error.Failure("Ошибка запроса"));

async Task<Result<int>> GetFallbackDataAsync() => Result.Success(42);

var result = await GetPrimaryDataAsync().OrElseAsync(GetFallbackDataAsync);
Console.WriteLine(result.Value); // 42
```

### Комбинация синхронного и асинхронного подхода

```csharp
async Task<Result<int>> GetPrimaryDataAsync() => Result.Failure<int>(Error.Failure("Ошибка запроса"));

Result<int> GetFallbackData() => Result.Success(100);

var result = await GetPrimaryDataAsync().OrElseAsync(GetFallbackData);
Console.WriteLine($"Альтернативное значение: {result.Value}");
```

> [!NOTE]
> `OrElseAsync` поддерживает параметр `configureAwait`, который можно использовать для управления контекстом выполнения.