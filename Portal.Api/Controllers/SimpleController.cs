using Microsoft.AspNetCore.Mvc;
using Portal.Api.Base;
using Portal.Api.Models;

namespace Portal.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleController : BaseController
{
    public SimpleController(IApiProcessor processor) : base(processor)
    {
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> Get([FromQuery]InputModel model) => await Execute(model);
}
