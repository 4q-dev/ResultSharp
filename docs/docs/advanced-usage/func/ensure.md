---  
title: Ensure и EnsureAsync  
description: Проверка успешности результата.  
---

# Ensure

Эти методы позволяют проверить успешность результата и выполнение заданного условия. Если результат успешен, но не удовлетворяет условию, возвращается указанная ошибка или используется ошибка по умолчанию.

## Когда использовать  

- Когда нужно убедиться, что результат успешен и удовлетворяет дополнительным условиям.  
- Для валидации данных после выполнения операции.  
- При необходимости возврата определённой ошибки в случае несоответствия условиям.  

## Как это работает  

- `Ensure` проверяет результат и выполняет функцию-условие. Если результат успешен и функция выполняется, возвращается исходный результат. В противном случае возвращается указанная ошибка.  
- `EnsureAsync` работает аналогично, но поддерживает асинхронные операции.  
- Оба метода возвращают исходный `Result`, что позволяет продолжать цепочку обработки.  

---

## Пример использования  

### Синхронный вариант  

```csharp
using ResultSharp;
using ResultSharp.Errors;
using ResultSharp.Extensions.FunctionalExtensions.Sync;

Result<int> result = SomeOperation();

// Проверка, что результат положительный
result.Ensure(
    predicate: value => value > 0,
    onFailure: Error.Validation("Значение должно быть положительным.")
);
```

### Асинхронный вариант  

```csharp
using ResultSharp;
using ResultSharp.Errors;
using ResultSharp.Extensions.FunctionalExtensions.Async;

async Task ProcessResultAsync()
{
    Task<Result<User>> user = GetUserFromDatabaseAsync();

    await result.EnsureAsync(
        predicate: u => NotInBlackList(u),
        onFailure: Error.Forbidden("Пользователь заблокирован.")
    )
    .ThenAsync(u => DoSomethingWithUser(u));
}
```