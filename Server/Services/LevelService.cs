using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using SharedLibrary.Database;
using SharedLibrary.Requests.LevelsController;
using SharedLibrary.Responses.LevelsController;

namespace Server.Services;

public class LevelService : ILevelService
{
    private readonly ServerDbContext _context;

    public LevelService(ServerDbContext context)
    {
        _context = context;
    }
    
    public ErrorType RegisterLevelResult(int userId, LevelChangeRequest request)
    {
        var result = _context.Levels.SingleOrDefault(l => l.UserId == userId && l.Num == request.Num);
        if (result != null)
            return UpdateLevelResult(userId, request);

        var newLevelData = new Level()
        {
            UserId = userId,
            Num = request.Num,
            Time = request.Time,
            Way = request.Way
        };
        
        _context.Add(newLevelData);
        _context.SaveChanges();
        
        return ErrorType.None;
    }

    public ErrorType UpdateLevelResult(int userId, LevelChangeRequest request)
    {
        var prevLevelData = _context.Levels.FirstOrDefault(l => l.UserId == userId && l.Num == request.Num);
        if (prevLevelData == null)
            return ErrorType.LevelDataDontFound;

        if(request.Time > prevLevelData.Time)
            return ErrorType.InvalidLevelData;

        prevLevelData.Time = request.Time;
        prevLevelData.Way = request.Way;

        _context.SaveChanges();
        
        return ErrorType.None;    
    }

    public List<LevelDataResponse> TakePlayerLevelsData(int userId)
    {
        var levels = _context.Levels.Where(level => level.UserId == userId);

        List<LevelDataResponse> levelsDatas = levels.Select(level => new LevelDataResponse(level)).ToList();
        
        return levelsDatas;
    }

    public TakeLeaderboardPageResponse TakeGlobalLeaderboardPage(int levelNum, int userId)
    {
        var result = _context.Levels.Include(u => u.User).Where(l => l.Num == levelNum);
        var newResult = result.OrderBy(l => l.Time);
        var chunks = newResult.ToList().Chunk(10);
        
        var playerResult = newResult.FirstOrDefault(l => l.UserId == userId);
        if (playerResult is null) return new TakeLeaderboardPageResponse();

        int chunkIndex = 0;
        Level[]? chunk = null;
        foreach (var someChunk in chunks)
        {
            if (someChunk.Contains(playerResult))
            {
                chunk = someChunk;
                break;
            }

            chunkIndex++;
        }
        
        if (chunk is null) return new TakeLeaderboardPageResponse();

        var rows = chunk
            .Select((t, i) => new LeaderboardRowResponse(10 * chunkIndex + i + 1, t));

        var finalResult = new TakeLeaderboardPageResponse(rows.ToList());
        return finalResult;
    }

    public TakeLeaderboardPageResponse TakeFriendsLeaderboardPage(int levelNum, int userId)
    {
        var playerResult = _context.Levels
            .Include(l => l.User)
            .SingleOrDefault(l => l.UserId == userId);
        if(playerResult is null) return new TakeLeaderboardPageResponse();
        
        var friendsIds1 = _context.FriendPairs
            .Where(fp => fp.User1Id == userId)
            .Select(fp => fp.User2Id);
        var friendsIds2 = _context.FriendPairs
            .Where(fp => fp.User2Id == userId)
            .Select(fp => fp.User1Id);

        var friendsIds = friendsIds1.Concat(friendsIds2);
        
        var friendsResults = _context.Levels
            .Include(l => l.User)
            .Where(l => l.Num == levelNum && friendsIds.Contains(l.User.Id));

        var result = friendsResults.ToList();
        result.Add(playerResult);
        var newResult = result.OrderBy(l => l.Time);
        var chunks = newResult.ToList().Chunk(10);
        
        int chunkNum = 0;
        Level[]? chunk = null;
        foreach (var someChunk in chunks)
        {
            if (someChunk.Contains(playerResult))
            {
                chunk = someChunk;
                break;
            }

            chunkNum++;
        }
        
        if (chunk is null) return new TakeLeaderboardPageResponse();

        var rows = chunk
            .Select((t, i) => new LeaderboardRowResponse(10 * chunkNum + i + 1, t));

        var finalResult = new TakeLeaderboardPageResponse(rows.ToList());
        return finalResult;
    }

    private const float WaysTimeStep = 0.5f;

    public List<string> TakeNearWays(int levelNum, int userId)
    {
        var res = _context.Levels.SingleOrDefault(l => l.UserId == userId && l.Num == levelNum);
        if (res is null)
            return new List<string>();

        var levelsRes = _context.Levels.Where(
            l =>
                l.Num == levelNum &&
                l.Time < res.Time &&
                l.Time >= res.Time - WaysTimeStep);

        var result = levelsRes.OrderBy(r => Guid.NewGuid()).Take(5);
        var someRes = result?.Select(l => l.Way);
        return
            someRes is null
                ? new List<string>()
                : someRes.ToList();
    }
}

public interface ILevelService
{
    public ErrorType RegisterLevelResult(int userId, LevelChangeRequest request);
    public ErrorType UpdateLevelResult(int userId, LevelChangeRequest request);
    public List<LevelDataResponse> TakePlayerLevelsData(int userId);
    public TakeLeaderboardPageResponse TakeGlobalLeaderboardPage(int levelNum, int userId);
    public TakeLeaderboardPageResponse TakeFriendsLeaderboardPage(int levelNum, int userId);
    public List<string> TakeNearWays(int levelNum, int userId);
}