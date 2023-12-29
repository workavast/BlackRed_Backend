using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary.Requests.LevelsController;
using SharedLibrary.Responses.LevelsController;
using SharedLibrary.ServicesResults;

namespace Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LevelsController : ControllerBase
{
    private readonly ILevelService _levelService;
    
    public LevelsController(ILevelService levelService)
    {
        _levelService = levelService;
    }
    
    [Authorize]
    [HttpPost("RegisterLevelResult")]
    public IActionResult RegisterLevelResult(LevelChangeRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest();
        
        var userId = int.Parse(value);
        
        var result = _levelService.RegisterLevelResult(userId, request);
        
        if(result == LevelServiceResult.Ok) 
            return Ok();
        else 
            return BadRequest();
    }
    
    [Authorize]
    [HttpPost("UpdateLevelResult")]
    public IActionResult UpdateLevelResult(LevelChangeRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest();
        
        var userId = int.Parse(value);
        var result = _levelService.UpdateLevelResult(userId, request);

        switch (result)
        {
            case (LevelServiceResult.Ok):
                return Ok();
            case (LevelServiceResult.LevelDontFound):
                return BadRequest("LevelDontFound");
            default:
                return BadRequest("Un expected error");
        }
    }
    
    [Authorize]
    [HttpGet("TakePlayerLevelsData")]
    public IActionResult TakePlayerLevelsData()
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest();
        
        var userId = int.Parse(value);
        var result = _levelService.TakePlayerLevelsData(userId);
        
        return Ok(new TakePlayerLevelsDataResponse(result));
    }
    
    [Authorize]
    [HttpPost("TakeLeaderboardPage")]
    public IActionResult TakeLeaderboardPage(TakeLeaderboardPageRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest();
        
        var userId = int.Parse(value);
        var result = _levelService.TakeLeaderboardPage(request.LevelNum, userId);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("TakeNearWays")]
    public IActionResult TakeNearWays(TakeNearWaysRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest();
        
        var userId = int.Parse(value);
        var ways = _levelService.TakeNearWays(request.LevelNum, userId);
        
        return Ok(new TakeNearWaysResponse(ways));
    }
}