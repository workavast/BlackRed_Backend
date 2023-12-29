namespace SharedLibrary.Requests.LevelsController
{
    public class TakeNearWaysRequest
    {
        public int LevelNum { get; set; }
    
        public TakeNearWaysRequest()
        {
            LevelNum = default;
        }

        public TakeNearWaysRequest(int levelNum)
        {
            LevelNum = levelNum;
        }
    }
}