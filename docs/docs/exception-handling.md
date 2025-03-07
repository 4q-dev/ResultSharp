# Конфигурирование обработчика исключений

## Введение

Библиотека `ResultSharp` предоставляет механизм для обработки исключений, возникающих при выполнении операций. По умолчанию, любое исключение преобразуется в объект `Error` с сообщением исключения и кодом ошибки `ErrorCode.Failure`. Однако, вы можете настроить собственный обработчик исключений для более гибкой обработки различных типов исключений.

## Настройка обработчика исключений

Для настройки обработчика исключений используется глобальная конфигурация `ResultConfigurationGlobal`. Обработчик исключений представляет собой функцию, которая принимает исключение (`Exception`) и возвращает объект ошибки (`Error`).

### Базовая конфигурация

```csharp
using ResultSharp.Configuration;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

// Настройка глобального обработчика исключений
new ResultConfigurationGlobal().Configure(options =>
{
    options.ExceptionHandlerConfiguration.Configure(config =>
    {
        config.ExceptionHandler = exception => 
            new Error("Произошла ошибка: " + exception.Message, ErrorCode.Failure);
    });
});
```

### Дифференцированная обработка различных типов исключений

Вы можете настроить обработчик для разных типов исключений, возвращая различные коды ошибок или сообщения:

```csharp
using ResultSharp.Configuration;
using ResultSharp.Errors;
using ResultSharp.Errors.Enums;

// Настройка обработчика с дифференциацией типов исключений
new ResultConfigurationGlobal().Configure(options =>
{
    options.ExceptionHandlerConfiguration.Configure(config =>
    {
        config.ExceptionHandler = exception =>
        {
            // Определяем код ошибки в зависимости от типа исключения
            var errorCode = exception switch
            {
                ArgumentException => ErrorCode.Validation,
                InvalidOperationException => ErrorCode.Conflict,
                FileNotFoundException => ErrorCode.NotFound,
                UnauthorizedAccessException => ErrorCode.Unauthorized,
                _ => ErrorCode.Failure
            };

            // Возвращаем объект ошибки с соответствующим кодом
            return new Error(exception.Message, errorCode);
        };
    });
});
```

## Использование обработчика исключений

После настройки обработчика исключений, он будет автоматически использоваться в методах `Try` и `TryAsync` класса `Result`:

```csharp
// Использование обработчика исключений в методе Try
var result = Result.Try(() => 
{
    // Код, который может вызвать исключение
    throw new ArgumentException("Неверное значение параметра");
});

// Проверка результата
if (!result.IsSuccess)
{
    // Обработка ошибки
    Console.WriteLine($"Ошибка: {result.Errors.First().Message}");
    Console.WriteLine($"Код ошибки: {result.Errors.First().ErrorCode}");
}
```

### Переопределение обработчика для конкретного вызова

Вы также можете переопределить глобальный обработчик исключений для конкретного вызова метода `Try`:

```csharp
// Переопределение обработчика для конкретного вызова
var result = Result.Try(() => 
{
    // Код, который может вызвать исключение
    throw new Exception("Специфическая ошибка");
}, 
exception => new Error("Специальная обработка: " + exception.Message, ErrorCode.ImATeapot));
```

## Примеры использования
> [!NOTE]
> Примеры ниже демонстрируют обработку исключений в учебных целях. В реальной бизнес-логике рекомендуется избегать выбрасывания исключений, поскольку это противоречит концепции Result паттерна. Метод `Result.Try()` следует применять только для обработки внешнего кода, который потенциально может выбросить исключение.

### Пример 1: Обработка исключений валидации

```csharp
// Настройка обработчика для исключений валидации
new ResultConfigurationGlobal().Configure(options =>
{
    options.ExceptionHandlerConfiguration.Configure(config =>
    {
        config.ExceptionHandler = exception =>
        {
            if (exception is ArgumentException argEx)
            {
                return new Error($"Ошибка валидации: {argEx.Message}", ErrorCode.Validation);
            }
            
            return new Error(exception.Message, ErrorCode.Failure);
        };
    });
});

// Использование в бизнес-логике
public Result ValidateUser(User user)
{
    return Result.Try(() =>
    {
        if (string.IsNullOrEmpty(user.Name))
            throw new ArgumentException("Имя пользователя не может быть пустым", nameof(user.Name));
            
        if (user.Age < 18)
            throw new ArgumentException("Пользователь должен быть совершеннолетним", nameof(user.Age));
    });
}
```

### Пример 2: Обработка исключений доступа к базе данных

```csharp
// Настройка обработчика для исключений базы данных
new ResultConfigurationGlobal().Configure(options =>
{
    options.ExceptionHandlerConfiguration.Configure(config =>
    {
        config.ExceptionHandler = exception =>
        {
            if (exception is SqlException sqlEx)
            {
                return sqlEx.Number switch
                {
                    // Ошибка подключения
                    -1 => new Error("Не удалось подключиться к базе данных", ErrorCode.Unavailable),
                    // Нарушение уникального ключа
                    2627 => new Error("Запись с таким ключом уже существует", ErrorCode.Conflict),
                    // Нарушение внешнего ключа
                    547 => new Error("Невозможно удалить запись, так как на неё есть ссылки", ErrorCode.Conflict),
                    // Другие ошибки SQL
                    _ => new Error($"Ошибка базы данных: {sqlEx.Message}", ErrorCode.Failure)
                };
            }
            
            return new Error(exception.Message, ErrorCode.Failure);
        };
    });
});
```

### Пример 3: Обработка исключений в асинхронных операциях

```csharp
// Использование обработчика в асинхронных методах
public async Task<Result<User>> GetUserByIdAsync(int userId)
{
    return await Result.TryAsync(async () =>
    {
        // Асинхронный код, который может вызвать исключение
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"Пользователь с ID {userId} не найден");
            
        return user;
    });
}
```

## Заключение

Настройка обработчика исключений в `ResultSharp` позволяет гибко обрабатывать различные типы исключений и преобразовывать их в информативные объекты ошибок. Это помогает создавать более надежные и предсказуемые приложения, где ошибки обрабатываются единообразно и предоставляют полезную информацию для отладки и пользовательского интерфейса.

Помните, что конфигурация обработчика исключений является глобальной для всего приложения, поэтому рекомендуется настраивать его при запуске приложения, до начала выполнения бизнес-логики.
