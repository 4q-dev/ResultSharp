---
title: Логгирование результатов
---

# Логгирование

> [!WARNING]  
> Прежде чем начать использовать логгирование, убедитесь, что вы установили адаптер для своего логгера и правильно настроили его. Подробнее о настройке читайте [здесь](~/docs/getting-started.md).

## Обзор

Логгирование результатов позволяет отслеживать состояние выполнения операций, библиотека предоставляет как стандартный набор методов логгирования, таких как `LogTrace`, `LogDebug` и т.д., так и специфичные методы для результатов: `LogIfSucces`, `LogIfFailure`

## Как это работает

- Базовые функции для логгирования по типу `LogDebug` производят логгирование при любом статусе результата
- Функция `LogIfSuccess` производит логгирование только в том случае, если результат операции успешен
- Функция `LogIfFailure` производит логгирование только в том случае, если результат операции не успешен
- Все функции логгирования возвращают исходный результат, что бы продолжить цепочку обработки

---

### Базовые методы логгирования

> [!NOTE]
> Все базовые методы логгирования принимают следующие параметры
> - `message` - выводимое сообщение, может содержать шаблон для подстановки значений, например: `value: {val}`
> - `context` - контекст, в котором производится логгирование
> - `args` - массив аргументов, применяемых для форматтирования шаблона

#### LogTrace

Логгирует результат с уровнем `Trace`. Этот уровень используется для наиболее детализированных сообщений.

```csharp
// Выведет: 
// trce: MyLogger[0]
//       Operation executed. 11, False
result.LogTrace("Operation executed. {arg1}, {arg2}", "message context", 11, false); 
```

#### LogDebug

Логгирует результат с уровнем `Debug`. Используется для отладки в процессе разработки.

```csharp
result.LogDebug("Debug information for operation.");
```

#### LogInformation

Логгирует результат с уровнем `Information`. Применяется для отслеживания общего потока выполнения приложения.

```csharp
result.LogInformation("Operation completed successfully.");
```

#### LogWarning

Логгирует результат с уровнем `Warning`. Подходит для отображения неожиданных ситуаций, которые не останавливают выполнение.

```csharp
result.LogWarning("Operation completed with warnings.");
```

#### LogError

Логгирует результат с уровнем `Error`. Используется для отображения ошибок, которые приводят к остановке текущего выполнения.

```csharp
result.LogError("Operation failed due to error.");
```

#### LogCritical

Логгирует результат с уровнем `Critical`. Применяется для критических сбоев, требующих немедленного вмешательства.

```csharp
result.LogCritical("Critical failure encountered.");
```

---

### Условное логгирование

#### LogIfSuccess

Логгирует сообщение только в случае успешного выполнения операции (`IsSuccess`).

```csharp
result.LogIfSuccess("Operation completed successfully.");
```

#### LogIfFailure

Логгирует сообщение только в случае ошибки (`IsFailure`). Также доступен вариант, который автоматически собирает и логгирует все сообщения об ошибках.

```csharp
result.LogIfFailure("Operation failed.");
```

```csharp
// Вывод:
// fail: MyLogger[0]
//       some failure message
//       not found message

result = Result.Failure(
    Error.Failure("some failure message"), 
    Error.NotFound("not found message")
);

result.LogIfFailure();
```

---

## Использование с `Result<T>`

> [!TIP]
> Все перечисленные методы работают как с `Result`, так и с `Result<T>`, но для `Result<T>` есть несколько особенностей

```csharp
// Вывод:
// info: MyLogger[0]
//       value 10
// info: MyLogger[0]
//       value 100
// info: MyLogger[0]
//       some message witout any args

// В качестве args принимет Result<int>.Value
Result<int>.Success(10).LogIfSuccess("value {0}"); 

// В качестве args передаются параметры, Result<int>.Value игнорируется
Result<int>.Success(10).LogIfSuccess("value {0}", "context", ResultSharp.Logging.LogLevel.Information, 100);

// Строка без аргументов
Result<int>.Success(10).LogIfSuccess("some message witout any args");
```

## Уровни логгирования

```csharp
enum LogLevel
{
    Trace,
    Debug,
    Information,
    Warning,
    Error,
    Critical
}
```
