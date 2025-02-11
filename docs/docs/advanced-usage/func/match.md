---
title: Match
description: Выполнение заданных действий в зависимости от результата операции.
---

# Match

`Match` — это метод, который позволяет выполнить разные действия в зависимости от успешности результата. Если операция завершилась успешно, вызывается одно действие, если произошла ошибка — другое.

## Когда использовать

- Когда нужно обработать успешный и неудачный результат без явных проверок `IsSuccess`.
- Для удобного выполнения побочных эффектов, например, логирования или отображения уведомлений.
- При написании чистого и читаемого кода без вложенных `if`-проверок.

---

## Пример использования

### Синхронный вариант

```csharp
Result<int> result = SomeOperation();

result.Match(
    onSuccess: value => Console.WriteLine($"Успешный результат: {value}"),
    onFailure: errors => Console.WriteLine($"Ошибка: {string.Join(", ", errors)}")
);
```

### Асинхронный вариант

```csharp
async Task ProcessResultAsync()
{
    var result = await SomeAsyncOperation();

    await result.MatchAsync(
        onSuccess: async value =>
        {
            Console.WriteLine($"Успешный результат: {value}");
            await Task.Delay(100); // Имитация асинхронной обработки
        },
        onFailure: async errors =>
        {
            Console.WriteLine($"Ошибка: {string.Join(", ", errors)}");
            await Task.Delay(100); // Имитация асинхронной обработки ошибки
        }
    );
}
```

> [!NOTE]
> Все асинхронные методы библиотеки поддерживают параметр `configureAwait`, который по умолчанию равен `true`. Этот параметр влияет на `Task.ConfigureAwait(configureAwait)`.
