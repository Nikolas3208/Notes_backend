using Microsoft.AspNetCore.Mvc;
using Notes.API.Contracts;
using Notes.Core.Abstractions;
using Notes.Core.Models;

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

    [HttpGet]
    public async Task<ActionResult<List<UserResponce>>> Get()
    {
        var users = await _usersService.Get();

        var usersResponce = users
            .Select(u => new UserResponce(u.Id, u.FirstName, u.Name, u.Email))
            .ToList();

        return Ok(usersResponce);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] UserRequest request)
    {
        var passwordHash = _passwordHasher.Generate(request.Password);
        
        var (error, user) =
            Core.Models.User.Create(Guid.NewGuid(), request.FirstName, request.Name, request.Email, passwordHash);

        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        var id = await _usersService.Create(user);

        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] UserRequest request)
    {
        var passwordHash = _passwordHasher.Generate(request.Password);

        var userId = await _usersService.Update(id, request.FirstName, request.Name, request.Email, passwordHash);

        return Ok(userId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
        var userId = await _usersService.Delete(id);

        return userId;
    }
}