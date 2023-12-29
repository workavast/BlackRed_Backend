namespace SharedLibrary.Requests.FriendsController
{
    public class CancelFriendReqRequest
    {
        public int RequestId { get; set; }
        
        public CancelFriendReqRequest()
        {
            RequestId = default;
        }
        
        public CancelFriendReqRequest(int requestId)
        {
            RequestId = requestId;
        }
    }
}