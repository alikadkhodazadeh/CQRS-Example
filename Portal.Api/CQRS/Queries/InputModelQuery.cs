global using FluentValidation;
global using Microsoft.OpenApi.Attributes;
global using Portal.Api.Helpers;

namespace Portal.Api.CQRS.Queries;

[Cached]
public class InputModelQuery : IRequest<InputModel>
{
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string? Email { get; set; }
}
