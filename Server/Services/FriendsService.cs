using Microsoft.EntityFrameworkCore;
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

    public bool SendRequest(int userId, string friendName)
    {
        var friend = _context.Users
            .SingleOrDefault(u => u.Name == friendName);
        
        if (friend is null)
            return false;

        var friendId = friend.Id;
        
        var res = _context.FriendRequests
            .SingleOrDefault(fr => fr.SenderId == userId && fr.RecipientId == friendId);

        if (res != null)
            return false;
     
        var req = new FriendRequest()
        {
            SenderId = userId, 
            RecipientId = friendId
        };
        _context.FriendRequests.Add(req);
        _context.SaveChanges();

        return true;
    }

    public bool AcceptRequest(int requestId)
    {
        var res = _context.FriendRequests
            .SingleOrDefault(fr => fr.Id == requestId);

        if (res is null)
            return false;

        var fp = new FriendPair()
        {
            User1Id = res.SenderId,
            User2Id = res.RecipientId
        };

        _context.FriendRequests.Remove(res);
        _context.FriendPairs.Add(fp);
        _context.SaveChanges();
        
        return true;
    }

    public bool DeAcceptRequest(int requestId)
    {
        var res = _context.FriendRequests
            .SingleOrDefault(fr => fr.Id == requestId);

        if (res is null)
            return false;

        _context.FriendRequests.Remove(res);
        _context.SaveChanges();
        
        return true;
    }

    public bool CancelFriendRequest(int requestId)
    {
        var res = _context.FriendRequests
            .SingleOrDefault(fr => fr.Id == requestId);

        if (res is null)
            return false;

        _context.FriendRequests.Remove(res);
        _context.SaveChanges();
        
        return true;
    }

    public bool DeleteFriend(int friendPairId)
    {
        var res = _context.FriendPairs
            .SingleOrDefault(fp => fp.Id == friendPairId);

        if (res is null)
            return false;

        _context.FriendPairs.Remove(res);
        _context.SaveChanges();
        
        return true;
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

        var finRes = res1.Concat(res2);

        return new TakeFriendsResponse(finRes.ToList());
    }

    public TakeFriendReqsResponse TakeFromMeRequests(int userId)
    {
        var res = _context.FriendRequests
            .Include(fr => fr.Sender)
            .Include(fr => fr.Recipient)
            .Where(fr => fr.SenderId == userId)
            .Select(fr => new FriendRequestResponse(fr));

        return new TakeFriendReqsResponse(res.ToList());
    }

    public TakeFriendReqsResponse TakeToMeRequests(int userId)
    {
        var res = _context.FriendRequests
            .Include(fr => fr.Sender)
            .Include(fr => fr.Recipient)
            .Where(fr => fr.RecipientId == userId)
            .Select(fr => new FriendRequestResponse(fr));

        return new TakeFriendReqsResponse(res.ToList());
    }
}

public interface IFriendsService
{
    public bool SendRequest(int userId, string friendName);

    public bool AcceptRequest(int requestId);

    public bool DeAcceptRequest(int requestId);

    public bool CancelFriendRequest(int requestId);

    public bool DeleteFriend(int friendPairId);

    public TakeFriendsResponse TakeFriends(int userId);

    public TakeFriendReqsResponse TakeFromMeRequests(int userId);

    public TakeFriendReqsResponse TakeToMeRequests(int userId);
}