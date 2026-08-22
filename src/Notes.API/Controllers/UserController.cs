using Microsoft.AspNetCore.Mvc;
using Notes.API.Contracts.User;
using Notes.Core.Abstractions;

namespace Notes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IPasswordHasher _passwordHasher;
    
    public UserController(IUsersService usersService, IPasswordHasher passwordHasher)
    {
        _usersService = usersService;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserRequest loginUser)
    {
        string token = await _usersService.Login(loginUser.Email, loginUser.Password);
        
        HttpContext.Response.Cookies.Append("jwt-token", token);
        
        return Ok(token);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest registerUser)
    {
        string error = await _usersService.Register(
            registerUser.FirstName,
            registerUser.LastName,
            registerUser.Email,
            registerUser.Password);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        if (!id.Equals(userId))
            return Unauthorized();
        
        var passwordHash = _passwordHasher.Generate(request.Password);

        await _usersService.Update(
            id,
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash);

        return Ok();
    }
}