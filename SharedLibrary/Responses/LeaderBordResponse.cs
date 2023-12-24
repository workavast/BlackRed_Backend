namespace SharedLibrary.Responses;

public class LeaderBordResponse
{
    public List<LeaderBordRowResponse> Data { get; set; }

    public LeaderBordResponse()
    {
        Data = new List<LeaderBordRowResponse>();
    }
    
    public LeaderBordResponse(List<LeaderBordRowResponse> data)
    {
        Data = data;
    }
}