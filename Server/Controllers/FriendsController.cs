using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
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
    [HttpPost(nameof(SendFriendReq))]
    public IActionResult SendFriendReq(SendFriendReqRequest sendFriendReqRequest)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return BadRequest("no NameIdentifier");
        
        var res = _friendsService.SendRequest(int.Parse(userId), sendFriendReqRequest.UserName);
        if (!res)
            return BadRequest("result is null");

        return Ok();
    }

    [Authorize]
    [HttpPost(nameof(AcceptFriendReq))]
    public IActionResult AcceptFriendReq(AcceptFriendReqRequest acceptFriendReqRequest)
    {
        var res = _friendsService.AcceptRequest(acceptFriendReqRequest.RequestId);
        if (!res)
            return BadRequest();

        return Ok();
    }
    
    [Authorize]
    [HttpPost(nameof(DeAcceptFriendReq))]
    public IActionResult DeAcceptFriendReq(DeAcceptFriendReqRequest deAcceptFriendReqRequest)
    {
        var res = _friendsService.DeAcceptRequest(deAcceptFriendReqRequest.RequestId);
        if (!res)
            return BadRequest();

        return Ok();
    }
    
    [Authorize]
    [HttpPost(nameof(CancelFriendReq))]
    public IActionResult CancelFriendReq(CancelFriendReqRequest cancelFriendReqRequest)
    {
        var res = _friendsService.CancelFriendRequest(cancelFriendReqRequest.RequestId);
        if (!res)
            return BadRequest();

        return Ok();
    }
    
    [Authorize]
    [HttpPost(nameof(DeleteFriend))]
    public IActionResult DeleteFriend(DeleteFriendRequest deleteFriendRequest)
    {
        var res = _friendsService.DeleteFriend(deleteFriendRequest.RequestId);
        if (!res)
            return BadRequest();

        return Ok();
    }
    
    [Authorize]
    [HttpGet(nameof(TakeFriends))]
    public IActionResult TakeFriends()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return BadRequest();
        
        var res = _friendsService.TakeFriends(int.Parse(userId));

        return Ok(res);
    }
    
    [Authorize]
    [HttpGet(nameof(TakeFromMeRequests))]
    public IActionResult TakeFromMeRequests()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return BadRequest();
        
        var res = _friendsService.TakeFromMeRequests(int.Parse(userId));

        return Ok(res);
    }
    
    [Authorize]
    [HttpGet(nameof(TakeToMeRequests))]
    public IActionResult TakeToMeRequests()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return BadRequest();
        
        var res = _friendsService.TakeToMeRequests(int.Parse(userId));

        return Ok(res);
    }
}