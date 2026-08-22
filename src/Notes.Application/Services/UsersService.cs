using Notes.Core.Abstractions;
using Notes.Core.Models;

namespace Notes.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<List<User>> Get()
    {
        return await _usersRepository.Get();
    }

    public async Task<Guid> Create(User user)
    {
        return await _usersRepository.Create(user);
    }

    public async Task<Guid> Update(Guid id, string firstName, string name, string email, string passwordHash)
    {
        return await _usersRepository.Update(id, firstName, name, email, passwordHash);
    }

    public async Task<Guid> Delete(Guid id)
    {
        return await _usersRepository.Delete(id);
    }
}