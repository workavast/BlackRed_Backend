using SharedLibrary.Database;

namespace SharedLibrary.Responses.LevelsController
{
    public class LevelDataResponse
    {
        public int Num { get; set; }
        public float Time { get; set; }
        public string Way { get; set; }

        public LevelDataResponse()
        {
            Num = default;
            Time = default;
            Way = default;
        }
    
        public LevelDataResponse(Level level)
        {
            Num = level.Num;
            Time = level.Time;
            Way = level.Way;
        }
    }
}