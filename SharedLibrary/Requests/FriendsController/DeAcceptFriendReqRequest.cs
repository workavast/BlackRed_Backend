namespace SharedLibrary.Requests.FriendsController
{
    public class DeAcceptFriendReqRequest
    {
        public int RequestId { get; set; }
        
        public DeAcceptFriendReqRequest()
        {
            RequestId = default;
        }
        
        public DeAcceptFriendReqRequest(int requestId)
        {
            RequestId = requestId;
        }
    }
}