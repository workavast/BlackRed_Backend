namespace SharedLibrary.Requests.FriendsController
{
    public class SendFriendReqRequest
    {
        public string UserName { get; set; }

        public SendFriendReqRequest()
        {
            UserName = default;
        }
        
        public SendFriendReqRequest(string userName)
        {
            UserName = userName;
        }
    }
}