# Конвертация Result в IActionResult

## Введение

Библиотека `ResultSharp.HttpResult` предоставляет удобные методы расширения для преобразования объектов типа `Result` и `Result<T>` в `IActionResult`. Это позволяет легко интегрировать монадный подход обработки ошибок с ASP.NET Core контроллерами, обеспечивая единообразное и элегантное преобразование результатов операций в HTTP-ответы.

Это позволяет:
- Унифицировать обработку ошибок в вашем приложении
- Автоматически преобразовывать коды ошибок в HTTP-статусы
- Сократить количество шаблонного кода в контроллерах
- Повысить читаемость и поддерживаемость кода

## Установка

Для начала работы с `ResultSharp.HttpResult` необходимо установить соответствующий пакет:

```sh
dotnet add package 4q-dev.ResultSharp.HttpResult
```

## Основные возможности

Библиотека предоставляет следующие методы расширения:

1. `ToResponse()` для объектов типа `Result`
2. `ToResponse()` для объектов типа `Result<T>`
3. `ToResponseAsync()` для асинхронных операций, возвращающих `Task<Result>`
4. `ToResponseAsync()` для асинхронных операций, возвращающих `Task<Result<T>>`

## Преобразование кодов ошибок в HTTP-статусы

При преобразовании `Result` в `IActionResult`, коды ошибок из `ErrorCode` автоматически преобразуются в соответствующие HTTP-статусы:

- Если код ошибки соответствует стандартному HTTP-статусу (например, `ErrorCode.NotFound` = 404), используется этот статус
- Для нестандартных кодов ошибок применяется следующая логика:
  - `ErrorCode.Validation` преобразуется в `400 Bad Request`
  - Остальные коды ошибок преобразуются в `500 Internal Server Error`

## Примеры использования

### Пример 1: Базовое использование с пустым Result

```csharp
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.HttpResult;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet("success")]
    public IActionResult GetSuccess()
    {
        // Возвращает HTTP 200 OK без тела ответа
        return Result.Success().ToResponse();
    }

    [HttpGet("failure")]
    public IActionResult GetFailure()
    {
        // Возвращает HTTP 500 Internal Server Error с информацией об ошибке
        return Result.Failure().ToResponse();
    }
}
```

### Пример 2: Использование с Result<T>

```csharp
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.HttpResult;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        Result<User> result = _userService.GetUserById(id);

        // Если успешно - возвращает HTTP 200 OK с объектом пользователя
        // Если ошибка - возвращает соответствующий HTTP-статус с информацией об ошибке
        return result.ToResponse();
    }

    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return Result<User>.Failure(Error.BadRequest("Некорректные данные запроса")).ToResponse();
        }

        Result<User> result = _userService.CreateUser(request);
        return result.ToResponse();
    }
}
```

### Пример 3: Использование с различными кодами ошибок

```csharp
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.HttpResult;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        Result<Product> result = _productService.GetProductById(id);
        
        if (!result.IsSuccess)
        {
            // Если продукт не найден, возвращаем 404 Not Found
            if (id <= 0)
            {
                return Result<Product>.Failure(Error.NotFound($"Продукт с ID {id} не найден")).ToResponse();
            }
            
            // Если нет доступа, возвращаем 403 Forbidden
            if (!_productService.HasAccess(User, id))
            {
                return Result<Product>.Failure(Error.Forbidden("Нет доступа к продукту")).ToResponse();
            }
        }
        
        return result.ToResponse();
    }
}
```

### Пример 4: Использование с асинхронными операциями

```csharp
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.HttpResult;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderAsync(int id)
    {
        // Асинхронный метод, возвращающий Task<Result<Order>>
        Task<Result<Order>> resultTask = _orderService.GetOrderByIdAsync(id);
        
        // Преобразование асинхронного результата в IActionResult
        return await resultTask.ToResponseAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest request)
    {
        // Проверка валидации модели
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => Error.BadRequest(e.ErrorMessage));
                
            return Result<Order>.Failure(errors.ToArray()).ToResponse();
        }

        // Асинхронное создание заказа
        Task<Result<Order>> resultTask = _orderService.CreateOrderAsync(request);
        
        // Преобразование асинхронного результата в IActionResult
        return await resultTask.ToResponseAsync();
    }
}
```