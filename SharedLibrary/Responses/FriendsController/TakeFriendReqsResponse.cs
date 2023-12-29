using System.Collections.Generic;

namespace SharedLibrary.Responses.FriendsController
{
    public class TakeFriendReqsResponse
    {
        public List<FriendRequestResponse> FriendRequests { get; set; }
        
        public TakeFriendReqsResponse()
        {
            FriendRequests = new List<FriendRequestResponse>();
        }
        
        public TakeFriendReqsResponse(List<FriendRequestResponse> friendRequest)
        {
            FriendRequests = friendRequest;
        }
    }
}