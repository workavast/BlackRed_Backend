namespace SharedLibrary
{
    public enum ErrorType
    {
        None = 0,
        
        NoNameIdentifier = 10,
            
        InvalidLogin = 20,
        InvalidPassword = 30,
        UserWithTisNameIsExist = 40,
        
        LevelDataDontFound = 50,
        InvalidLevelData = 60,
        
        UserDontFound = 70,
        InvalidUserId = 80,
        FriendRequestToSelf = 90,
        FriendRequestExist = 100,
        FriendRequestDontFound = 110,
        FriendPairDontFound = 120
    }
}