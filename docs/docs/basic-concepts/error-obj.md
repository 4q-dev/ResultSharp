---
title: Объект Error
description: Работа с объектом ошибок
---

# Ошибки `Error`

В `ResultSharp` ошибки представлены объектом `Error`, который содержит:

- Код ошибки (`ErrorCode`) — определяет тип ошибки.  
- Сообщение (`Message`) — описание проблемы.  

Этот объект нужен, чтобы передавать информацию об ошибках без использования исключений.  

## Пример использования

Допустим, у нас есть сервис, который ищет пользователей по ID. Если пользователь не найден, мы возвращаем `Error.NotFound()`:  

```csharp
public Result<User> GetUserById(int id)
{
    var user = database.FindUser(id);
    
    if (user == null)
        return Error.NotFound($"User with ID {id} not found.");

    return user; // Автоматически конвертируется в Result<User> 
                 // Так же можно использовать функциональное API возврата: 
                 // return Result<User>.Success(user);
}
```

## Основные виды ошибок

Для удобства есть предустановленные ошибки:  

```csharp
// В качестве сообщения можно передавать свое или использовать
// значение по умолчанию.

var notFoundError = Error.NotFound(); // "The requested resource was not found."
var conflictError = Error.Conflict("This email is already in use.");
var baseFailure = Error.Failure("Some failure message.")
...
```

> [!NOTE]
> `ErrorCode` может отражать HTTP статус код, если это возможно, например: для NotFound - значение 404,
> но для Failure - 0, так как это обобщенное значение не является HTTP статусом.

Вы можете создавать свои ошибки, передавая нужное сообщение и код:  

```csharp
var customError = new Error("I'm a teapot", ErrorCode.ImATeapot);
```
