using Microsoft.EntityFrameworkCore;
using SharedLibrary.Database;
using SharedLibrary.Requests.LevelsController;
using SharedLibrary.Responses.LevelsController;
using SharedLibrary.ServicesResults;

namespace Server.Services;

public class LevelService : ILevelService
{
    private readonly ServerDbContext _context;

    public LevelService(ServerDbContext context)
    {
        _context = context;
    }
    
    public LevelServiceResult RegisterLevelResult(int userId, LevelChangeRequest request)
    {
        var result = _context.Levels.SingleOrDefault(l => l.UserId == userId && l.Num == request.Num);
        if (result != null)
            return LevelServiceResult.LevelExist;

        var newLevelData = new Level()
        {
            UserId = userId,
            Num = request.Num,
            Time = request.Time,
            Way = request.Way
        };
        
        _context.Add(newLevelData);
        _context.SaveChanges();
        
        return LevelServiceResult.Ok;
    }

    public LevelServiceResult UpdateLevelResult(int userId, LevelChangeRequest request)
    {
        var prevLevelData = _context.Levels.FirstOrDefault(l => l.UserId == userId && l.Num == request.Num);
        if (prevLevelData == null)
            return LevelServiceResult.LevelDontFound;

        prevLevelData.Time = request.Time;
        prevLevelData.Way = request.Way;

        _context.SaveChanges();
        
        return LevelServiceResult.Ok;    
    }

    public List<LevelDataResponse> TakePlayerLevelsData(int userId)
    {
        var levels = _context.Levels.Where(level => level.UserId == userId);

        List<LevelDataResponse> levelsDatas = levels.Select(level => new LevelDataResponse(level)).ToList();
        
        return levelsDatas;
    }

    public TakeLeaderboardPageResponse TakeLeaderboardPage(int levelNum, int userId)
    {
        var result = _context.Levels.Include(u => u.User).Where(l => l.Num == levelNum);
        var newResult = result.OrderBy(l => l.Time);
        var chunks = newResult.ToList().Chunk(10);
        
        var playerResult = newResult.FirstOrDefault(l => l.UserId == userId);
        if (playerResult is null) return new TakeLeaderboardPageResponse();

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
        
        //TODO: create Leaderboard response
        if (chunk == null) return new TakeLeaderboardPageResponse();

        List<LeaderboardRowResponse> rows = new List<LeaderboardRowResponse>();
        for (int i = 0; i < chunk.Length; i++)
            rows.Add(new LeaderboardRowResponse(10 * chunkNum + i + 1, chunk[i].User.Name, chunk[i].Time));
            
        var finalResult = new TakeLeaderboardPageResponse(rows);
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

        // var ways = new List<string>();
        // for (int i = 1; i <= 5; i++)
        // {
        //     var way = TakeNearWay(levelNum, res.Time - WaysTimeStep * i);
        //     
        //     if (way != null) 
        //         ways.Add(way);
        // }

        // return ways;
    }

    // private const float TimeRange = 0.25f;
    // private string? TakeNearWay(int levelNum, float levelTime)
    // {
    //     var result = _context.Levels.FirstOrDefault(
    //         l => 
    //         l.Num == levelNum && 
    //         l.Time <= levelTime && 
    //         l.Time >= levelTime - TimeRange);
    //
    //     return result?.Way;
    // }
}

public interface ILevelService
{
    public LevelServiceResult RegisterLevelResult(int userId, LevelChangeRequest request);
    public LevelServiceResult UpdateLevelResult(int userId, LevelChangeRequest request);
    public List<LevelDataResponse> TakePlayerLevelsData(int userId);
    public TakeLeaderboardPageResponse TakeLeaderboardPage(int levelNum, int userId);
    public List<string> TakeNearWays(int levelNum, int userId);
}