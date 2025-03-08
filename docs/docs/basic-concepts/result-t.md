# Объект Result<T>

## Введение

`Result<T>` представляет собой объект, который инкапсулирует результат операции, которая может завершиться успешно или с ошибкой. Этот подход является альтернативой использованию исключений и позволяет более явно обрабатывать ошибки в коде.

## Основные свойства

- `IsSuccess` - указывает, успешно ли выполнена операция
- `IsFailure` - указывает, завершилась ли операция с ошибкой
- `Value` - значение результата (доступно только если `IsSuccess == true`)
- `Errors` - коллекция ошибок (доступна только если `IsFailure == true`)

> [!WARNING]
> Если вы попробуете получить `Value`, если `IsSuccess == false`, то вы получите исключение `InvalidOperationException`.

## Создание успешного результата


### 1. Через статический метод

```csharp
// Создание успешного результата с значением
var successResult = Result<string>.Success("Операция выполнена успешно");
var anotherSuccessResult = Result.Success(0) // Эквивалентно Result<int>.Success(0)
```

### 2. Через неявное преобразование

```csharp
// Неявное преобразование значения в Result<T>
Result<int> successResult = 42;
```

## Создание результата с ошибкой

### 1. Через статический метод с одной ошибкой

```csharp
// Создание результата с одной ошибкой
var errorResult = Result<int>.Failure(Error.NotFound("Запись не найдена"));
```

### 2. Через статический метод с несколькими ошибками

```csharp
// Создание результата с несколькими ошибками
var errors = new List<Error>
{
    Error.Validation("Имя не может быть пустым"),
    Error.Validation("Возраст должен быть положительным числом")
};

var errorResult = Result<User>.Failure(errors);
```

### 3. Через неявное преобразование  

```csharp
// Создание результата с ошибкой "Не найдено"
Result<User> notFoundResult = Error.NotFound("Пользователь не найден");

// Создание результата с ошибкой валидации
Result<Order> validationResult = Error.ValidationError("Сумма заказа должна быть больше нуля");

// Создание результата с ошибкой доступа
Result<Document> forbiddenResult = Error.Forbidden("У вас нет прав для доступа к документу");
```

## Примеры использования

### Пример 1: Получение данных пользователя

```csharp
public Result<UserProfile> GetProfile(int userId)
{
    var user = database.FindUser(userId);
    if (user == null)
        return Error.NotFound("User profile not found.");

    return new UserProfile(user);
}
```

### Пример 2: Обработка результата

```csharp
var result = userService.GetProfile(42);

if (result.IsSuccess)
{
    Console.WriteLine($"Имя пользователя: {result.Value.Name}");
    Console.WriteLine($"Email: {result.Value.Email}");
}
else
{
    Console.WriteLine($"Ошибка: {result.Errors.First().Message}");
}
```

### Пример 3: Валидация данных

```csharp
public Result<User> CreateUser(UserDto userDto)
{
    var errors = new List<Error>();
    
    if (string.IsNullOrEmpty(userDto.Name))
        errors.Add(Error.Validation("Имя не может быть пустым"));
        
    if (userDto.Age < 18)
        errors.Add(Error.Validation("Возраст должен быть не менее 18 лет"));
        
    if (errors.Any())
        return Result<User>.Failure(errors);
        
    var user = new User
    {
        Id = Guid.NewGuid(),
        Name = userDto.Name,
        Age = userDto.Age
    };
    
    return user;
}
```
