# Преобразование обычных значений в Result

## Обзор

Библиотека `ResultSharp` предоставляет методы расширения для выстраивания цепочек операций, но для этого необходимо использовать объекты типа `Result` или `Result<T>`. При работе со сторонним кодом, который возвращает обычные значения, можно использовать методы `ToResult` и `ToResultAsync` для выстраивания цепочек операций.

## Когда использовать

1. При необходимости интеграции обычного кода с кодом, использующим паттерн Result
2. Когда требуется выполнить валидацию значений перед их дальнейшим использованием

## Доступные методы

### 1. Преобразование значения в Result

Преобразует любое значение типа `T` в успешный результат `Result<T>`.
```csharp
var value = 1;
var result = value.ToResult();
```

### 2. Преобразование значения в Result с валидацией

Этот метод преобразует значение типа `T` в `Result<T>`, применяя к нему набор правил валидации. Если хотя бы одно правило не выполняется, возвращается ошибка.

> [!NOTE]
> По умолчанию используется ошибка `Error.Validation`. При необходимости использовать другую ошибку, укажите её в параметре `onFailure`.

```csharp
var value = 10;
var rules = new List<Predicate<int>>
{
    x => x > 0,
    x => x < 100
};

var result = value.ToResult(rules);
```

### 3. Асинхронное преобразование задачи в Result

Этот метод асинхронно преобразует задачу, возвращающую значение типа `T`, в задачу, возвращающую успешный результат `Result<T>`.

```csharp
var task = Task.FromResult(1);
var result = await task.ToResultAsync();
```

### 4. Асинхронное преобразование задачи в Result с валидацией

Этот метод асинхронно преобразует задачу, возвращающую значение типа `T`, в задачу, возвращающую `Result<T>`, применяя к результату задачи набор правил валидации.

```csharp
var task = Task.FromResult(1);
var rules = new List<Predicate<int>>
{
    x => x > 0,
    x => x < 100
};
var result = await task.ToResultAsync(rules);
```

## Примеры использования

### Простое преобразование значения

```csharp
// Преобразование строки в Result<string>
string nickname = "LightChimera";
var nicknameResult = nickname
    .ToResult()
    .Ensure(nickname => nickname.Length > 0, Error.Validation("Nickname is empty"))
    .Ensure(nickname => nickname.Length < 100, Error.Validation("Nickname is too long"))
    .Map(nickname => nickname.ToUpper());

// Использование результата
if (nicknameResult.IsSuccess)
{
    Console.WriteLine($"Beautifull nickname: {nicknameResult.Value}");
}
```
> [!NOTE]
> При использовании методов `ToResult` и `ToResultAsync` с правилами валидации, в случае неуспешной проверки, возвращается единая ошибка из параметра `onFailure` (по умолчанию `Error.Validation`), независимо от того, какое конкретное правило валидации не было выполнено. Для получения специфичных ошибок для каждого правила валидации рекомендуется использовать цепочку методов `Ensure` и `EnsureAsync`, которые позволяют задать индивидуальные сообщения об ошибках для каждого условия.

### Преобразование с валидацией

```csharp
// Определение правил валидации для возраста
var ageValidationRules = new List<Predicate<int>>
{
    age => age >= 0,
    age => age <= 120
};

// Преобразование возраста в Result<int> с валидацией
int age = 25;
var ageResult = age
    .ToResult(ageValidationRules)
    .LogIfSuccess("Valid age: {age}");

// Использование с пользовательской ошибкой
int invalidAge = -5;
var invalidAgeResult = invalidAge
    .ToResult(
        ageValidationRules,
        Error.Validation("Age must be between 0 and 120")
    )
    .LogErrorMessages();
```

### Асинхронное преобразование

> [!NOTE]
> `ToResultAsync` поддерживает параметр `configureAwait` (по умолчанию `true`), который позволяет контролировать поведение `ConfigureAwait` для асинхронных операций.

```csharp
// Асинхронное получение данных пользователя
async Task<User> GetUserAsync(int userId)
{
    // Имитация получения пользователя из базы данных
    await Task.Delay(100);
    return new User { Id = userId, Name = "Иван" };
}

// Преобразование задачи в Result<User>
async Task<Result<User>> GetUserResultAsync(int userId)
{
    Task<User> userTask = GetUserAsync(userId);
    return await userTask.ToResultAsync()
        .Ensure(user => user != null, Error.NotFound("User not found"));
}
```

### Асинхронное преобразование с валидацией

```csharp
// Правила валидации для пользователя
var userValidationRules = new List<Predicate<User>>
{
    user => user != null,
    user => !string.IsNullOrEmpty(user.Name),
    user => user.Id > 0
};

// Асинхронное получение пользователя с валидацией
async Task<Result<User>> GetValidatedUserResultAsync(int userId)
{
    Task<User> userTask = GetUserAsync(userId);
    return await userTask.ToResultAsync(userValidationRules);
}
```
