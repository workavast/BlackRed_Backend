namespace SharedLibrary.Responses;

public class LeaderBordRowResponse
{
    public string Name { get; set; }
    public float Time { get; set; }

    public LeaderBordRowResponse()
    {
        Name = default;
        Time = default;
    }
    
    public LeaderBordRowResponse(string name, float time)
    {
        Name = name;
        Time = time;
    }
}