namespace SharedLibrary.Responses;

public class WaysResponse
{
    public List<string> Ways { get; set; }

    public WaysResponse(List<string> ways)
    {
        Ways = ways;
    }
}