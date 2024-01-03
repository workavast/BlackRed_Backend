namespace SharedLibrary.Requests.FriendsController
{
    public abstract class FriendReqBase
    {
        public int RequestId { get; set; }

        protected FriendReqBase()
        {
            RequestId = default;
        }

        protected FriendReqBase(int requestId)
        {
            RequestId = requestId;
        }    
    }
}