using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary.Requests;
using SharedLibrary.Responses;
using SharedLibrary.ServicesResults;

namespace Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LevelController : ControllerBase
{
    private readonly ILevelService _levelService;
    
    public LevelController(ILevelService levelService)
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
        var result = _levelService.CreateNewLevelData(userId, request);
        
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
        
        return Ok(new LevelsDatasResponse(){LevelsData = result});
    }
    
    [Authorize]
    [HttpPost("TakeLeaderboardPage")]
    public IActionResult TakeLeaderboardPage(LeaderBordRequest request)
    {
        _levelService.TakeLeaderboardPage(request.LevelNum, request.PlayerLevelTime);
        return Ok();
    }
    
    [Authorize]
    [HttpPost("TakeNearWays")]
    public IActionResult TakeNearWays(TakeNearWaysRequest request)
    {
        var ways = _levelService.TakeNearWays(request.LevelNum, request.LevelTime);
        
        return Ok(new WaysResponse(ways));
    }
}