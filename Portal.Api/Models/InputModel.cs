using Portal.Api.Base;

namespace Portal.Api.Models;

public class InputModel : IApiResult<string>
{
    public string? FirstName { get; set; }
    public string? SurName { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {SurName}";
    }
}
