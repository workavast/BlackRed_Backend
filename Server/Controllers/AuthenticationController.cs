using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary.Paths;
using SharedLibrary.Requests.AuthenticationController;
using SharedLibrary.Responses.AuthenticationController;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [AllowAnonymous]
    [HttpPost(AuthenticationControllerPaths.Register)]
    public IActionResult Register(AuthenticationRequest request)
    {
        var (success, content) = _authenticationService.Register(request.UserLogin, request.UserPassword);

        if (!success) return BadRequest(content);
        
        return Login(request);
    }
    
    [AllowAnonymous]
    [HttpPost(AuthenticationControllerPaths.Login)]
    public IActionResult Login(AuthenticationRequest request)
    {
        var (success, content) = _authenticationService.Login(request.UserLogin, request.UserPassword);

        if (!success) return BadRequest(content);
        
        return Ok(new AuthenticationResponse(content));
    }
}