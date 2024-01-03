using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary;
using SharedLibrary.Paths;
using SharedLibrary.Requests.FriendsController;

namespace Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FriendsController : ControllerBase
{
    private readonly IFriendsService _friendsService;
    
    public FriendsController(IFriendsService friendsService)
    {
        _friendsService = friendsService;
    }

    [Authorize]
    [HttpPost(FriendsControllerPaths.SendFriendReq)]
    public IActionResult SendFriendReq(SendFriendReqRequest sendFriendReqRequest)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.SendRequest(userId, sendFriendReqRequest.UserName);
        
        if (result != ErrorType.None)
            return BadRequest(result);

        return Ok();
    }

    [Authorize]
    [HttpPost(FriendsControllerPaths.AcceptFriendReq)]
    public IActionResult AcceptFriendReq(AcceptFriendReqRequest acceptFriendReqRequest)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.AcceptRequest(userId, acceptFriendReqRequest.RequestId);
        if (result != ErrorType.None)
            return BadRequest(result);

        return Ok();
    }
    
    [Authorize]
    [HttpPost(FriendsControllerPaths.DeAcceptFriendReq)]
    public IActionResult DeAcceptFriendReq(DeAcceptFriendReqRequest deAcceptFriendReqRequest)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.DeAcceptRequest(userId, deAcceptFriendReqRequest.RequestId);
        if (result != ErrorType.None)
            return BadRequest(result);

        return Ok();
    }
    
    [Authorize]
    [HttpPost(FriendsControllerPaths.CancelFriendReq)]
    public IActionResult CancelFriendReq(CancelFriendReqRequest cancelFriendReqRequest)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.CancelFriendRequest(userId, cancelFriendReqRequest.RequestId);
        if (result != ErrorType.None)
            return BadRequest(result);

        return Ok();
    }
    
    [Authorize]
    [HttpPost(FriendsControllerPaths.DeleteFriend)]
    public IActionResult DeleteFriend(DeleteFriendRequest deleteFriendRequest)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.DeleteFriend(userId, deleteFriendRequest.RequestId);
        if (result != ErrorType.None)
            return BadRequest(result);

        return Ok();
    }
    
    [Authorize]
    [HttpGet(FriendsControllerPaths.TakeFriends)]
    public IActionResult TakeFriends()
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.TakeFriends(userId);

        return Ok(result);
    }
    
    [Authorize]
    [HttpGet(FriendsControllerPaths.TakeFromMeRequests)]
    public IActionResult TakeFromMeRequests()
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.TakeFromMeRequests(userId);

        return Ok(result);
    }
    
    [Authorize]
    [HttpGet(FriendsControllerPaths.TakeToMeRequests)]
    public IActionResult TakeToMeRequests()
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return BadRequest(ErrorType.NoNameIdentifier);
        var userId = int.Parse(userIdStr);
        
        var result = _friendsService.TakeToMeRequests(userId);

        return Ok(result);
    }
}