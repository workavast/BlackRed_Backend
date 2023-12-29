using System.Collections.Generic;

namespace SharedLibrary.Responses.FriendsController
{
    public class TakeFriendsResponse
    {
        public List<FriendPairResponse> FriendPairs { get; set; }

        public TakeFriendsResponse()
        {
            FriendPairs = new List<FriendPairResponse>();
        }
        
        public TakeFriendsResponse(List<FriendPairResponse> friendPairs)
        {
            FriendPairs = friendPairs;
        }
    }
}