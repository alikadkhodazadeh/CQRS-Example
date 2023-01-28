using Portal.Api.Base;
using Portal.Api.Models;

namespace Portal.Api.CQRS.Queries;

public class SimpleQueryHandler : IApiHandler<InputModel, string>
{
    public Task<string> Handle(InputModel request)
    {
        return Task.FromResult(request.ToString());
    }
}
