using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Portal.Api.CQRS.Queries;
using Portal.Api.Helpers;
using System.Reflection;

namespace Portal.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    private IMemoryCache? _cache;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    protected IMemoryCache Cache => _cache ??= HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

    [NonAction]
    public async Task<IActionResult> Execute<TResponse>(IRequest<TResponse> request)
    {
        return Ok(new
        {
            data = await Mediator.Send(request),
            isSuccess = true
        });
    }

    private Tuple<bool,string> GetCachedRequest<TResponse>(IRequest<TResponse> request)
    {
        var cachedRequests = Cache.GetOrCreate("CachedRequests", _ =>
        {
            return typeof(InputModelQuery).Assembly.GetTypes()
                .Where(x => x.GetCustomAttribute<CachedAttribute>(true) is not null)
                .ToList();
        });

        var res = cachedRequests?.SingleOrDefault(r => r == request.GetType());

        if (res is not null)
            return new(true, CacheKeyManager.GetCasheKey(request.GetType().Name));
        return new(false, string.Empty);
    }
}