namespace SharedLibrary.Requests.FriendsController
{
    public class AcceptFriendReqRequest
    {
        public int RequestId { get; set; }
        
        public AcceptFriendReqRequest()
        {
            RequestId = default;
        }
        
        public AcceptFriendReqRequest(int requestId)
        {
            RequestId = requestId;
        }
    }
}