using System.Collections.Generic;

namespace SharedLibrary.Responses.FriendsController
{
    public class TakeFriendRequestResponse
    {
        public List<FriendRequestResponse> FriendRequests { get; set; }
        
        public TakeFriendRequestResponse()
        {
            FriendRequests = new List<FriendRequestResponse>();
        }
        
        public TakeFriendRequestResponse(List<FriendRequestResponse> friendRequest)
        {
            FriendRequests = friendRequest;
        }
    }
}