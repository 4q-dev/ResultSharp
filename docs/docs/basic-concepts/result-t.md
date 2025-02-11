---
title: Объект Result<T>
description: Резульат операции, возвращающий значение
---

# Результат операции `Result<T>`

Когда метод выполняется успешно, он может вернуть результат `T`. Но если что-то пошло не так, вместо исключения лучше вернуть `Result<T>` с ошибкой.  

## Пример использования

Рассмотрим сервис, который получает данные профиля:  

```csharp
public Result<UserProfile> GetProfile(int userId)
{
    var user = database.FindUser(userId);
    if (user == null)
        return Error.NotFound("User profile not found.");

    return new UserProfile(user);
}
```

Теперь вызывающий код может безопасно обработать результат:  

```csharp
var result = GetProfile(42);

if (result.IsSuccess)
    Console.WriteLine($"User name: {result.Value.Name}");
else
    Console.WriteLine($"Error: {result.Errors.First().Message}");
```
