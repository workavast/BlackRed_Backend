namespace SharedLibrary.Requests.LevelsController
{
    public class TakeLeaderboardPageRequest
    {
        public int LevelNum { get; set; }

        public TakeLeaderboardPageRequest()
        {
            LevelNum = default;
        }

        public TakeLeaderboardPageRequest(int levelNum)
        {
            LevelNum = levelNum;
        }
    }
}