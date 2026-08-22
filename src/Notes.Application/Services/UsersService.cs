using Notes.Core.Abstractions;
using Notes.Core.Models;

namespace Notes.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UsersService(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _usersRepository.GetByEmail(email);

        if (user is null)
            return string.Empty;

        if (!_passwordHasher.Verify(password, hash: user.PasswordHash))
            return string.Empty;

        var token = _jwtProvider.Generate(user.Id);

        return token;
    }

    public async Task<string> Register(string firstName, string lastName, string email, string password)
    {
        string passwordHash = _passwordHasher.Generate(password);

        var (error, user) = User.Create(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            passwordHash);

        if (!string.IsNullOrEmpty(error))
            return error;
        
        error = await _usersRepository.Add(user);

        return error;
    }

    public async Task Update(Guid id, string firstName, string name, string email, string passwordHash)
    {
        await _usersRepository.Update(id, firstName, name, email, passwordHash);
    }
}