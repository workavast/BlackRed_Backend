namespace SharedLibrary.Requests.FriendsController
{
    public class DeleteFriendRequest
    {
        public int RequestId { get; set; }
        
        public DeleteFriendRequest()
        {
            RequestId = default;
        }
        
        public DeleteFriendRequest(int requestId)
        {
            RequestId = requestId;
        }    
    }
}