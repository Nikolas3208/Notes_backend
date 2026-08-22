using Notes.Core.Models;

namespace Notes.Core.Abstractions;

public interface IUsersService
{
    Task<List<User>> Get();
    
    Task<Guid> Create(User user);

    Task<Guid> Update(Guid id, string firstName, string name, string email, string passwordHash);

    Task<Guid> Delete(Guid id);
}