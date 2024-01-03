using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using SharedLibrary.Database;
using SharedLibrary.Responses.FriendsController;

namespace Server.Services;

public class FriendsService : IFriendsService
{
    private readonly ServerDbContext _context;

    public FriendsService(ServerDbContext context)
    {
        _context = context;
    }

    public ErrorType SendRequest(int userId, string friendName)
    {
        var friend = _context.Users
            .SingleOrDefault(u => u.Name == friendName);
        
        if (friend is null)
            return ErrorType.UserDontFound;
        if (friend.Id == userId)
            return ErrorType.FriendRequestToSelf;
        
        var result = _context.FriendRequests
            .SingleOrDefault(fr => fr.SenderId == userId && fr.RecipientId == friend.Id);

        if (result != null)
            return ErrorType.FriendRequestExist;

        var friendRequest = new FriendRequest()
        {
            SenderId = userId, 
            RecipientId = friend.Id
        };
        _context.FriendRequests.Add(friendRequest);
        _context.SaveChanges();

        return ErrorType.None;
    }

    public ErrorType AcceptRequest(int userId, int requestId)
    {
        var friendRequest = _context.FriendRequests
            .SingleOrDefault(fr => fr.Id == requestId);

        if (friendRequest is null)
            return ErrorType.FriendRequestDontFound;
        if (friendRequest.RecipientId != userId)
            return ErrorType.InvalidUserId;
        
        var friendPair = new FriendPair()
        {
            User1Id = friendRequest.SenderId,
            User2Id = friendRequest.RecipientId
        };

        _context.FriendRequests.Remove(friendRequest);
        _context.FriendPairs.Add(friendPair);
        _context.SaveChanges();
        
        return ErrorType.None;
    }

    public ErrorType DeAcceptRequest(int userId, int requestId)
    {
        var friendRequest = _context.FriendRequests
            .SingleOrDefault(fr => fr.Id == requestId);

        if (friendRequest is null)
            return ErrorType.FriendRequestDontFound;
        if (friendRequest.RecipientId != userId)
            return ErrorType.InvalidUserId;
        
        _context.FriendRequests.Remove(friendRequest);
        _context.SaveChanges();
        
        return ErrorType.None;
    }

    public ErrorType CancelFriendRequest(int userId, int requestId)
    {
        var friendRequest = _context.FriendRequests
            .SingleOrDefault(fr => fr.Id == requestId);

        if (friendRequest is null)
            return ErrorType.FriendRequestDontFound;
        if (friendRequest.SenderId != userId)
            return ErrorType.InvalidUserId;

        _context.FriendRequests.Remove(friendRequest);
        _context.SaveChanges();
        
        return ErrorType.None;
    }

    public ErrorType DeleteFriend(int userId, int friendPairId)
    {
        var friendPair = _context.FriendPairs
            .SingleOrDefault(fp => fp.Id == friendPairId);

        if (friendPair is null)
            return ErrorType.FriendPairDontFound;
        if (friendPair.User1Id != userId && friendPair.User2Id != userId)
            return ErrorType.InvalidUserId;

        _context.FriendPairs.Remove(friendPair);
        _context.SaveChanges();
        
        return ErrorType.None;
    }

    public TakeFriendsResponse TakeFriends(int userId)
    {
        var res1 = _context.FriendPairs
            .Include(fp => fp.User2)
            .Where(fp => fp.User1Id == userId)
            .ToList()
            .Select(fp => new FriendPairResponse(fp.Id,fp.User2.Name));
        
        var res2 = _context.FriendPairs
            .Include(fp => fp.User1)
            .Where(fp => fp.User2Id == userId)
            .ToList()
            .Select(fp => new FriendPairResponse(fp.Id,fp.User1.Name));

        var friendPairResponses = res1.Concat(res2);

        return new TakeFriendsResponse(friendPairResponses.ToList());
    }

    public TakeFriendReqsResponse TakeFromMeRequests(int userId)
    {
        var friendRequestResponses = _context.FriendRequests
            .Include(fr => fr.Sender)
            .Include(fr => fr.Recipient)
            .Where(fr => fr.SenderId == userId)
            .Select(fr => new FriendRequestResponse(fr));

        return new TakeFriendReqsResponse(friendRequestResponses.ToList());
    }

    public TakeFriendReqsResponse TakeToMeRequests(int userId)
    {
        var friendRequestResponses = _context.FriendRequests
            .Include(fr => fr.Sender)
            .Include(fr => fr.Recipient)
            .Where(fr => fr.RecipientId == userId)
            .Select(fr => new FriendRequestResponse(fr));

        return new TakeFriendReqsResponse(friendRequestResponses.ToList());
    }
}

public interface IFriendsService
{
    public ErrorType SendRequest(int userId, string friendName);

    public ErrorType AcceptRequest(int userId, int requestId);

    public ErrorType DeAcceptRequest(int userId, int requestId);

    public ErrorType CancelFriendRequest(int userId, int requestId);

    public ErrorType DeleteFriend(int userId, int friendPairId);

    public TakeFriendsResponse TakeFriends(int userId);

    public TakeFriendReqsResponse TakeFromMeRequests(int userId);

    public TakeFriendReqsResponse TakeToMeRequests(int userId);
}