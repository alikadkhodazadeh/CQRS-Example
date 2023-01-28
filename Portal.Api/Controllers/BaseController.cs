using Microsoft.AspNetCore.Mvc;
using Portal.Api.Base;

namespace Portal.Api.Controllers;

public abstract class BaseController : Controller
{
    private readonly IApiProcessor _processor;

    public BaseController(IApiProcessor processor)
    {
        _processor = processor;
    }

    [NonAction]
    public async Task<IActionResult> Execute<TResponse>([FromBody] IApiResult<TResponse> model)
        => Ok(new { data = await _processor.Process(model), isSuccess = true });
}