using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.Errors;
using ResultSharp.HttpResult;

namespace ResultSharp.Tests.Integration.HttpResponse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("success-empty")]
        public IActionResult GetSuccessEmptyResult()
        {
            return Result.Success().ToResponse();
        }

        [HttpGet("failure-empty")]
        public IActionResult GetFailureEmptyResult()
        {
            return Result.Failure().ToResponse();
        }

        [HttpGet("failure-empty-404")]
        public IActionResult GetFailureEmpty404Result()
        {
            return Result.Failure(
                Error.NotFound(),
                Error.NotFound("Вторая ошибка")
            ).ToResponse();
        }


        [HttpGet("success-object")]
        public IActionResult GetSuccessIntResult()
        {
            return Result<SomeDataObject>.Success(new SomeDataObject()).ToResponse();
        }

        public class SomeDataObject()
        {
            public string Name { get; set; } = "Name";
            public int Age { get; set; } = 5;
        }

        [HttpGet("failure-object")]
        public IActionResult GetFailureObject()
        {
            var persone = new SomeDataObject();
            if (persone.Age < 18)
                return Result<SomeDataObject>.Failure(Error.BadRequest("Маленькая еще")).ToResponse();

            return Result<SomeDataObject>.Success(persone).ToResponse();
        }
    }
}
