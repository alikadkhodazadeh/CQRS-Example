using Microsoft.Extensions.Caching.Memory;

namespace Portal.Api.CQRS.Queries;

public class InputModelQueryHandler : QueryHandler<InputModelQuery, InputModel>
{
    private readonly IMemoryCache _cache;
    public InputModelQueryHandler(IMemoryCache cache) : base(cache)
    {
        _cache=cache;
    }

    public override Task<InputModel> Handle(InputModelQuery request, CancellationToken cancellationToken)
    {
        var res = GetCachedRequest(request);
        if (res.Item1)
        {
            var data = _cache.GetOrCreate<InputModel>(res.Item2, _ =>
            {
                return new("Ali", "Coder");
            });
            return Task.FromResult(data)!;
        }
        return Task.FromResult<InputModel>(default!);
    }
}

