using Microsoft.Extensions.Caching.Memory;
using Portal.Api.CQRS.Queries;
using System.Reflection;

namespace Portal.Api.Helpers;

public abstract class QueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cache;

    protected QueryHandler(IMemoryCache cache)
    {
        _cache = cache;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected Tuple<bool, string> GetCachedRequest(TRequest request)
    {
        var cachedRequests = _cache.GetOrCreate("CachedRequests", _ =>
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