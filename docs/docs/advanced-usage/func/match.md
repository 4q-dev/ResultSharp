---
title: Match
description: Выполнение заданных действий в зависимости от результата операции.
---

# Match

`Match` — это метод, который позволяет выполнить разные действия в зависимости от успешности результата. Если операция завершилась успешно, вызывается одно действие, если произошла ошибка — другое.

> [!TIP]
> Используйте `Match` вместо проверки `IsSuccess` с последующим `if-else`-оператором.

## Синхронные варианты

> [!NOTE] 
> Синхроные и асихнронные варианты методов расширения работают как для `Result`, так и для `Result<T>`.

```csharp
Result result = SomeOperation();

var matchResult = result.Match(
    onSuccess: () => {
        // Выполняем необходимые операции
        return Result.Success(10);
    },
    onFailure: errors => {
        // Логика обработки ошибок
        if (errors.First().ErrorCode == ErrorCode.NotFound)
            return Result.Success(-1);

        return Result<int>.Failure();
    }
);
```

## Асинхронные варианты

> [!NOTE]
> Делегат `onSuccess` в методе расширения для `Result<T>` принимает на вход значение `T` успешного результата.

```csharp
Task<Result<string>> result = GetSomeStringResultAsync();

var matchResult = await result.MatchAsync(
    val => {
        if (val == "Привет от LightChimera!")
            return "удачи поклинкодить!";
        return val;
    },
    errs => Result<string>.Failure(),
    configureAwait: false
);
```

> [!NOTE]
> Делегатам не обязательно возвращать дженерик-результат.

```csharp
Task<Result<string>> result = GetSomeStringResultAsync();

var matchResult = result.MatchAsync(
    ok => Result.Success(),
    err => Result.Failure()
);
```

> [!NOTE]
> При помощи параметра `configureAwait` можно управлять асинхронным контекстом, что особенно важно в UI-приложениях с главным управляющим потоком.