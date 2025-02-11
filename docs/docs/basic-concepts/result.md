---
title: Объект Result
description: Резульат операции
---

# Результат без данных `Result`

Если метод выполняет действие, но не возвращает данные, лучше использовать `Result`.  

## Пример использования

Допустим, у нас есть метод, который отправляет уведомление.  

```csharp
public Result SendNotification(User user)
{
    if (!user.HasEmail)
        return Error.Failure("User has no email.");

    emailService.Send(user.Email, "Hello!");
    return Result.Success();
}
```

Теперь вызывающий код может проверить, удалось ли отправить сообщение:  

```csharp
var result = SendNotification(user);

if (result.IsFailure)
    Console.WriteLine($"Failed to send email: {result.Errors.First().Message}");
```
