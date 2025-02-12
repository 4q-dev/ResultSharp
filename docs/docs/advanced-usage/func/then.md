---  
title: ThenAsync и Then  
description: Чейнинг операций на основе успешного результата с поддержкой асинхронности и синхронности.  
---

# ThenAsync и Then

`ThenAsync` и `Then` — это методы для цепочечного выполнения операций в зависимости от успешности результата. Они позволяют продолжать выполнение следующей операции только в случае успешного завершения предыдущей, избегая вложенных проверок `IsSuccess`.

## Когда использовать

- Когда нужно выполнить цепочку зависимых операций, каждая из которых зависит от успешности предыдущей.
- Чтобы избежать явных проверок `IsSuccess` и улучшить читаемость кода.
- Для удобной работы с асинхронными операциями в стиле цепочек.

## Как это работает

- `ThenAsync` используется для асинхронных операций, возвращающих `Task<Result>` или `Task<Result<T>>`.
- `Then` используется для синхронных операций, возвращающих `Result` или `Result<T>`.
- Оба метода продолжают цепочку только если результат успешный. В случае ошибки возвращается исходный результат с ошибками.

---

## Пример использования

### Асинхронный вариант (`ThenAsync`)

```csharp
async Task<Result<int>> GetUserIdAsync(string username)
{
    // Симуляция запроса в базу данных
    await Task.Delay(100);
    return username == "admin" 
        ? Result<int>.Success(1) 
        : Result<int>.Failure("Пользователь не найден");
}

async Task<Result<string>> GetUserProfileAsync(int userId)
{
    // Симуляция получения профиля пользователя
    await Task.Delay(100);
    return Result<string>.Success($"Профиль пользователя с ID: {userId}");
}

async Task ProcessUserProfileAsync(string username)
{
    var result = await GetUserIdAsync(username)
        .ThenAsync(id => GetUserProfileAsync(id));

    result.Match(
        onSuccess: profile => Console.WriteLine($"Профиль: {profile}"),
        onFailure: errors => Console.WriteLine($"Ошибка: {errors.SummaryErrorMessages()}")
    );
}
```

---

### Синхронный вариант (`Then`)

```csharp
Result<int> ParseNumber(string input)
{
    return int.TryParse(input, out var number)
        ? Result<int>.Success(number)
        : Result<int>.Failure("Некорректное число");
}

Result<int> DoubleNumber(int number)
{
    return Result<int>.Success(number * 2);
}

void ProcessNumber(string input)
{
    var result = ParseNumber(input)
        .Then(DoubleNumber);

    result.Match(
        onSuccess: doubled => Console.WriteLine($"Удвоенное значение: {doubled}"),
        onFailure: errors => Console.WriteLine($"Ошибка!}")
    );
}
```
