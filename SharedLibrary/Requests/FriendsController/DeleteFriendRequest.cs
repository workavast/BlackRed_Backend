namespace SharedLibrary.Requests.FriendsController
{
    public class DeleteFriendRequest : FriendReqBase
    {
        public DeleteFriendRequest() : base(){}
        
        public DeleteFriendRequest(int requestId) : base(requestId){}
    }
}