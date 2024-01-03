using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary;
using SharedLibrary.Paths;
using SharedLibrary.Requests.LevelsController;
using SharedLibrary.Responses.LevelsController;

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
    [HttpPost(LevelsControllerPaths.RegisterLevelResult)]
    public IActionResult RegisterLevelResult(LevelChangeRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest(ErrorType.NoNameIdentifier);
        
        var userId = int.Parse(value);
        
        var result = _levelService.RegisterLevelResult(userId, request);
        
        if(result != ErrorType.None) 
            return BadRequest(result);

        return Ok();
    }
    
    [Authorize]
    [HttpPost(LevelsControllerPaths.UpdateLevelResult)]
    public IActionResult UpdateLevelResult(LevelChangeRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest(ErrorType.NoNameIdentifier);
        
        var userId = int.Parse(value);
        var result = _levelService.UpdateLevelResult(userId, request);

        if(result != ErrorType.None)
            return BadRequest(result);

        return Ok();
    }
    
    [Authorize]
    [HttpGet(LevelsControllerPaths.TakePlayerLevelsData)]
    public IActionResult TakePlayerLevelsData()
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest(ErrorType.NoNameIdentifier);
        
        var userId = int.Parse(value);
        var result = _levelService.TakePlayerLevelsData(userId);
        
        return Ok(new TakePlayerLevelsDataResponse(result));
    }
    
    [Authorize]
    [HttpPost(LevelsControllerPaths.TakeGlobalLeaderboardPage)]
    public IActionResult TakeGlobalLeaderboardPage(TakeLeaderboardPageRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest(ErrorType.NoNameIdentifier);
        
        var userId = int.Parse(value);
        var result = _levelService.TakeGlobalLeaderboardPage(request.LevelNum, userId);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost(LevelsControllerPaths.TakeFriendsLeaderboardPage)]
    public IActionResult TakeFriendsLeaderboardPage(TakeLeaderboardPageRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest(ErrorType.NoNameIdentifier);
        
        var userId = int.Parse(value);
        var result = _levelService.TakeFriendsLeaderboardPage(request.LevelNum, userId);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost(LevelsControllerPaths.TakeNearWays)]
    public IActionResult TakeNearWays(TakeNearWaysRequest request)
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value == null) return BadRequest(ErrorType.NoNameIdentifier);
        
        var userId = int.Parse(value);
        var ways = _levelService.TakeNearWays(request.LevelNum, userId);
        
        return Ok(new TakeNearWaysResponse(ways));
    }
}