namespace SharedLibrary.Responses
{
    public class FriendRequestResponse
    {
        public int RequestId { get; set; }

        public int SenderId { get; set; }
        public int RecipientId { get; set; }

        public string SenderName { get; set; }
        public string RecipientName { get; set; }

        public FriendRequestResponse()
        {
            RequestId = default;
            SenderId = default;
            RecipientId = default;
            SenderName = default;
            RecipientName = default;
        }
        
        public FriendRequestResponse(FriendRequest friendRequest)
        {
            RequestId = friendRequest.Id;
            SenderId = friendRequest.SenderId;
            RecipientId = friendRequest.RecipientId;

            SenderName = friendRequest.Sender.Name;
            RecipientName = friendRequest.Recipient.Name;
        }
    }
}