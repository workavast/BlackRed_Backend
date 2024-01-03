using SharedLibrary.Database;

namespace SharedLibrary.Responses.LevelsController
{
    public class LeaderboardRowResponse
    {
        public int Place { get; set; }
        public string UserName { get; set; }
        public float Time { get; set; }

        public LeaderboardRowResponse()
        {
            UserName = default;
            Time = default;
        }
    
        public LeaderboardRowResponse(int place, string userName, float time)
        {
            Place = place;
            UserName = userName;
            Time = time;
        }
        
        public LeaderboardRowResponse(int place, Level level)
        {
            Place = place;
            UserName = level.User.Name;
            Time = level.Time;
        }
    }
}