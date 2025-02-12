---  
title: Unwrap и UnwrapAsync  
description: Извлечение значения из результата с использованием значений по умолчанию или выбросом исключений в случае ошибки.  
---

# Unwrap и UnwrapOrDefault

`Unwrap` и `UnwrapAsync` — это методы, которые позволяют извлечь значение из результата (`Result`) или асинхронного результата (`Task<Result<T>>`).  
Если операция прошла успешно, возвращается значение.  
Если произошла ошибка, можно либо вернуть значение по умолчанию, либо выбросить исключение.  

## Когда использовать  

- Когда нужно получить значение из результата без явной проверки `IsSuccess`.  
- Для упрощения обработки результата с использованием значений по умолчанию.  
- Если требуется выбросить исключение в случае неудачи с указанием ошибок.  

## Как это работает  

- `UnwrapOrDefault` и `UnwrapOrDefaultAsync` возвращают значение, если операция успешна, или переданное значение по умолчанию, если произошла ошибка.  
- `Unwrap` и `UnwrapAsync` возвращают значение при успешном выполнении или выбрасывают исключение `InvalidOperationException` с сообщениями об ошибках в случае неудачи.

> [!WARNING]
> Мы советуем избегать использования метода `Unwrap`, так как выбрасывание исключений противоречит Result паттерну.

---

## Пример использования  

### Синхронный вариант  

```csharp
Result<int> result = SomeOperation();

// Получение значения или значение по умолчанию
int value = result.UnwrapOrDefault(42);
Console.WriteLine($"Результат: {value}");

// Получение значения или выброс исключения в случае ошибки
int valueOrException = result.Unwrap();
Console.WriteLine($"Результат: {valueOrException}");
```

### Асинхронный вариант  

```csharp
async Task ProcessResultAsync()
{
    Task<Result<int>> result = SomeAsyncOperation();

    // Получение значения или значение по умолчанию
    int value = await result.UnwrapOrDefaultAsync(42);
    Console.WriteLine($"Результат: {value}");

    // Получение значения или выброс исключения в случае ошибки
    int valueOrException = await result.UnwrapAsync();
    Console.WriteLine($"Результат: {valueOrException}");
}
```
