namespace SharedLibrary.Requests.FriendsController
{
    public class CancelFriendReqRequest : FriendReqBase
    {
        public CancelFriendReqRequest() : base(){}
        
        public CancelFriendReqRequest(int requestId) : base(requestId){}
    }
}