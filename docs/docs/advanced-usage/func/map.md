---
title: Map
description: Преобразование успешного результата в другой тип с помощью заданного действия.
---

# Map

`Map` — это метод, позволяющий преобразовать успешный результат (`Result<T>`) в новый тип, применяя заданную функцию. Если результат содержит ошибку, `Map` просто передает её дальше без изменений.

## Когда использовать

- Когда нужно изменить тип данных успешного результата.
- Чтобы избежать явных проверок `IsSuccess`, делая код чище и выразительнее.
- Для построения цепочек обработки результата без потери информации об ошибках.

---

## Пример использования

```csharp
Result<int> result = Result.Success(5);

// Преобразуем int в string, если результат успешен
Result<string> mappedResult = result.Map(value => $"Number: {value}");

Console.WriteLine(mappedResult.Value); // Выведет "Number: 5"
```

## Пример использоавния в асинхронном контексте

```csharp
var resultTask = Task.FromResult(Result<int>.Success(10));

// Асинхронное преобразование результата
int mappedResult = await resultTask.MapAsync(num => num * 10);

Console.WriteLine(mappedResult); // Выведет "100"
```

> [!NOTE]
> Все асинхронные методы библиотеки поддерживают параметр `configureAwait`, который по умолчанию равен `true`. Этот параметр влияет на `Task.ConfigureAwait(configureAwait)`.
