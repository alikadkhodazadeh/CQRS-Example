global using Portal.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.CQRS.Queries;

namespace Portal.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SimpleController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get() => await Execute(new InputModelQuery());

    [HttpPost]
    public async Task<IActionResult> Send(InputModelQuery query) => await Execute(query);
}

