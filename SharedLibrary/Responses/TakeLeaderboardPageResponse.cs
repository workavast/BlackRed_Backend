using System.Collections.Generic;

namespace SharedLibrary.Responses
{
    public class TakeLeaderboardPageResponse
    {
        public List<LeaderboardRowResponse> Rows { get; set; }

        public TakeLeaderboardPageResponse()
        {
            Rows = new List<LeaderboardRowResponse>();
        }
    
        public TakeLeaderboardPageResponse(List<LeaderboardRowResponse> rows)
        {
            Rows = rows;
        }
    }
}