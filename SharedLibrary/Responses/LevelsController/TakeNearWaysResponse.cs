using System.Collections.Generic;

namespace SharedLibrary.Responses.LevelsController
{
    public class TakeNearWaysResponse
    {
        public List<string> Ways { get; set; }

        public TakeNearWaysResponse()
        {
            Ways = default;
        }
    
        public TakeNearWaysResponse(List<string> ways)
        {
            Ways = ways;
        }
    }
}