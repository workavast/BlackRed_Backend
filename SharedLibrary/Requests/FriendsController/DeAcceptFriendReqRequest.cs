namespace SharedLibrary.Requests.FriendsController
{
    public class DeAcceptFriendReqRequest : FriendReqBase
    {
        public DeAcceptFriendReqRequest() : base(){}
        
        public DeAcceptFriendReqRequest(int requestId) : base(requestId){}
    }
}