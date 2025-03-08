# Объект Result

## Введение

`Result` представляет собой объект, который инкапсулирует результат операции, которая может завершиться успешно или с ошибкой, но не возвращает значение (`void`). Этот тип используется для методов, которые выполняют действие, но не возвращают данные.

## Основные свойства

- `IsSuccess` - указывает, успешно ли выполнена операция
- `IsFailure` - указывает, завершилась ли операция с ошибкой
- `Errors` - коллекция ошибок (доступна только если `IsFailure == true`)

> [!WARNING]
> Если вы попробуете получить `Errors` если `IsSuccess == true`, то вы получите исключение `InvalidOperationException`.

## Создание успешного результата

```csharp
// Создание успешного результата
var successResult = Result.Success();
```

## Создание результата с ошибкой

### 1. Через статический метод с одной ошибкой

```csharp
// Создание результата с одной ошибкой
var errorResult = Result.Failure(Error.Validation("Некорректные данные"));
```

### 2. Через статический метод с несколькими ошибками

```csharp
// Создание результата с несколькими ошибками
var errors = new List<Error>
{
    Error.Validation("Имя не может быть пустым"),
    Error.Validation("Возраст должен быть положительным числом")
};

var errorResult = Result.Failure(errors);
```

### 3. Через неявное преобразование  

```csharp
// Создание результата с ошибкой "Не найдено"
Result notFoundResult = Error.NotFound("Запись не найдена");

// Создание результата с ошибкой валидации
Result validationResult = Error.ValidationError("Некорректные данные формы");

// Создание результата с ошибкой доступа
Result forbiddenResult = Error.Forbidden("У вас нет прав для выполнения этой операции");
```

## Примеры использования

### Пример 1: Отправка уведомления

```csharp
public Result SendNotification(User user)
{
    if (!user.HasEmail)
        return Error.Failure("Пользователь не имеет email адреса.");

    emailService.Send(user.Email, "Привет!");
    return Result.Success();
}
```

### Пример 2: Обработка результата

```csharp
var result = notificationService.SendNotification(user);

if (result.IsSuccess)
{
    Console.WriteLine("Уведомление успешно отправлено");
}
else
{
    Console.WriteLine($"Ошибка при отправке уведомления: {result.Errors.First().Message}");
}
```

### Пример 3: Удаление записи

```csharp
public Result DeleteUser(int userId)
{
    var user = database.FindUser(userId);
    
    if (user == null)
        return Error.NotFound("Пользователь не найден");
        
    if (!currentUser.HasAdminRights)
        return Error.Forbidden("Недостаточно прав для удаления пользователя");
        
    database.DeleteUser(userId);
    return Result.Success();
}
```