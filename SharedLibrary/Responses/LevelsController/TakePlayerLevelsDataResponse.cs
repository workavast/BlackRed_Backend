using System.Collections.Generic;

namespace SharedLibrary.Responses.LevelsController
{
    public class TakePlayerLevelsDataResponse
    {
        public List<LevelDataResponse> LevelsData { get; set; }
    
        public TakePlayerLevelsDataResponse()
        {
            LevelsData = default;
        }

        public TakePlayerLevelsDataResponse(List<LevelDataResponse> levelsData)
        {
            LevelsData = levelsData;
        }
    }
}