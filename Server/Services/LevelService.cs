using SharedLibrary.Requests;
using SharedLibrary;
using SharedLibrary.Responses;
using SharedLibrary.ServicesResults;

namespace Server.Services;

public class LevelService : ILevelService
{
    private readonly ServerDbContext _context;

    public LevelService(ServerDbContext context)
    {
        _context = context;
    }
    
    public LevelServiceResult CreateNewLevelData(int userId, LevelChangeRequest request)
    {
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

    public List<Level> TakeLeaderboardPage(int levelNum, float playerLevelTime)
    {
        var result = _context.Levels.Where(l => l.Num == levelNum);
        var newResult = result.OrderBy(l => l.Time);
        
        var playerResult = newResult.FirstOrDefault(l => l.UserId == 2);
        
        var chunks = newResult.Chunk(10).Where(c => c.Contains(playerResult));
        var chunk = chunks.Where(c => c.Contains(playerResult));

        //TODO: create leaderboard response
        return chunk.First().ToList();
    }

    private const float WaysTimeStep = 0.5f;
    public List<string> TakeNearWays(int levelNum, float levelTime)
    {
        var ways = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            var way = TakeNearWay(levelNum, levelTime + WaysTimeStep * i);
            
            if (way != null) 
                ways.Add(way);
        }

        return ways;
    }

    private const float TimeRange = 0.25f;
    private string? TakeNearWay(int levelNum, float levelTime)
    {
        var result = _context.Levels.FirstOrDefault(
            l => 
            l.Num == levelNum && 
            l.Time <= levelTime && 
            l.Time >= levelTime - TimeRange);

        return result?.Way;
    }
}

public interface ILevelService
{
    public LevelServiceResult CreateNewLevelData(int userId, LevelChangeRequest request);
    public LevelServiceResult UpdateLevelResult(int userId, LevelChangeRequest request);
    public List<LevelDataResponse> TakePlayerLevelsData(int userId);
    public List<Level> TakeLeaderboardPage(int levelNum, float playerLevelTime);
    public List<string> TakeNearWays(int levelNum, float levelTime);
}