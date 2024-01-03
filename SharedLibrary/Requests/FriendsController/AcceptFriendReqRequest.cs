namespace SharedLibrary.Requests.FriendsController
{
    public class AcceptFriendReqRequest : FriendReqBase
    {
        public AcceptFriendReqRequest() : base(){}
        
        public AcceptFriendReqRequest(int requestId) : base(requestId){}
    }
}