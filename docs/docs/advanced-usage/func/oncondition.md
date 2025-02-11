---
title: OnSuccessAsync и OnFailureAsync
description: Выполнение действий при успешном или неудачном результате операции.
---

# OnSuccessc и OnFailure

Эти методы позволяют выполнять заданные действия при успешном (`OnSuccess`) или неудачном (`OnFailure`) результате выполнения операции. Они не изменяют сам результат, а просто выполняют переданное действие.

## Когда использовать

- Когда необходимо выполнить побочные эффекты (например, логирование, отправку уведомлений) в зависимости от результата.
- Если нужно избежать явных проверок `IsSuccess` или `IsFailure`.
- Для улучшения читаемости асинхронного кода.

## Как это работает

- `OnSuccess` выполняет переданное действие, если результат успешен.
- `OnFailure` выполняет переданное действие, если результат содержит ошибку.
- Оба метода возвращают исходный `Result`, позволяя продолжать цепочку обработки.

---

## Пример использования

### OnSuccess

```csharp
var result = SomeOperation();

result.OnSuccessAsync(() => Console.WriteLine("Операция завершена успешно."));
```

Использование с параметром:

```csharp
result.OnSuccessAsync(value => Console.WriteLine($"Полученное значение: {value}"));
```

---

### OnFailure

```csharp
var result = SomeOperation();

result.OnFailureAsync(() => Console.WriteLine("Ошибка выполнения операции."));
```

Использование с параметром:

```csharp
result.OnFailureAsync(errors => Console.WriteLine($"Ошибки: {string.Join(", ", errors)}"));
```

## Асинхронная поддержка

Методы поддерживают как синхронные, так и асинхронные действия:

```csharp
await result.OnSuccessAsync(async () =>
{
    Console.WriteLine("Операция завершена успешно.");
    await Task.Delay(100); // Имитация асинхронного действия
});
```

> [!NOTE]
> Все асинхронные методы поддерживают параметр `configureAwait`, который по умолчанию равен `true`. Этот параметр влияет на `Task.ConfigureAwait(configureAwait)`.