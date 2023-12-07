namespace SharedLibrary.Responses;

public class LevelDataResponse
{
    public int Num { get; set; }
    public float Time { get; set; }
    public string Way { get; set; }

    public LevelDataResponse(Level level)
    {
        Num = level.Num;
        Time = level.Time;
        Way = level.Way;
    }
}