---
title: Установка и конфигурация
description: Начало работы с библиотекой ResultSharp
---

# Установка и конфигурация

## Установка

Для начала работы с `ResultSharp` необходимо установить основной пакет:

```sh
dotnet add package 4q-dev.ResultSharp
```

---

## Подключение логирования

Логирование в `ResultSharp` позволяет автоматически фиксировать ошибки и события в вашем приложении, упрощая отладку и мониторинг. Использование встроенных адаптеров логирования дает возможность интеграции с популярными системами логирования, такими как `Microsoft.Extensions.Logging` и `Serilog`. Вы также можете реализовать собственный адаптер, если вам требуется специфичная обработка логов.

> [!IMPORTANT]
> Основная библиотека поддерживает методы логирование, но для их использования необходимо установить дополнительные пакеты с адаптерами под ваш логгер или создать собственный адаптер.

### [Microsoft Logging](#tab/microsoft-logging)

#### Установка Microsoft Logging адаптера

```sh
dotnet add package 4q-dev.ResultSharp.Logging.MicrosoftLogger
```

#### Конфигурация

```csharp
using Microsoft.Extensions.Logging;
using ResultSharp.Configuration;
using ResultSharp.Logging.MicrosoftLogger;

// Настройка Microsoft Logger
using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Мой логгер");

// Подключение логгера к ResultSharp
new ResultConfigurationGlobal().Configure(options =>
{
    options.LoggingConfiguration.Configure(logConfig => 
        logConfig.LoggingAdapter = new MicrosoftLoggingAdapter(logger)
    );
});
```

### [Serilog](#tab/serilog)

#### Установка Serilog адаптера

```sh
dotnet add package 4q-dev.ResultSharp.Logging.Serilog
```

#### Конфигурация

```csharp
using ResultSharp.Configuration;
using ResultSharp.Logging.Serilog;
using Serilog;

// Настройка Serilog
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Подключение логгера к ResultSharp
new ResultConfigurationGlobal().Configure(options =>
{
    options.LoggingConfiguration.Configure(logConfig =>
        logConfig.LoggingAdapter = new SerilogAdapter(logger)
    );
});
```

### [Собственный адаптер](#tab/custom)

#### Введение

Если вам не подходят готовые адаптеры логирования, предоставляемые библиотекой, вы можете реализовать свой собственный. Для этого создайте класс, реализующий интерфейс `ILoggingAdapter`.

#### Реализация адаптера

```csharp
using ResultSharp.Logging.Abstractions;

namespace MyApp.Logging; 

public class MyCustomLoggingAdapter : ILoggingAdapter
{
    public void Log(string message, LogLevel logLevel, string context, params object?[] args)
    {
        var messageWithArgs = args.Length > 0 ? string.Format(message, args) : message;
        Console.WriteLine($"{context.ToUpper()}: {messageWithArgs}");
    }
}
```

#### Конфигурация

```csharp
using ResultSharp.Configuration;
using MyApp.Logging;

// Подключение собственного адаптера
new ResultConfigurationGlobal().Configure(options =>
{
    options.LoggingConfiguration.Configure(logConfig =>
        logConfig.LoggingAdapter = new MyCustomLoggingAdapter()
    );
});
```

### [Без логгера](#tab/without-logging)

#### Введение

Вы также можете не использовать логирование, если в этом нет необходимости. По умолчанию логирование включено, но его можно отключить в конфигурации.

> [!WARNING]
> Если логирование не отключено, но вы попытаетесь использовать его без установленного адаптера, это приведет к `InvalidOperationException`.

#### Конфигурация

```csharp
using ResultSharp.Configuration;

// Отключение логирования
new ResultConfigurationGlobal().Configure(options =>
{
    options.EnableLogging = false;
});
```
