namespace SharedLibrary.Responses
{
    public class FriendPairResponse
    {
        public int Id { get; set; }
        public string FriendName { get; set; }
        
        public FriendPairResponse()
        {
            Id = default;
            FriendName = default;
        }
        
        public FriendPairResponse(int id, string friendName)
        {
            Id = id;
            FriendName = friendName;
        }
    }
}