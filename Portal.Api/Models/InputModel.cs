namespace Portal.Api.Models;

public class InputModel 
{
    public InputModel()
    {
        
    }
    public InputModel(string? firstName, string? surName)
    {
        FirstName = firstName;
        SurName = surName;
    }

    public string? FirstName { get; set; }
    public string? SurName { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {SurName}";
    }
}
