using Notes.Core.Models;

namespace Notes.Core.Abstractions;

public interface IUsersRepository
{
    Task<User?> GetByEmail(string email);
    
    Task<string> Add(User user);
    
    Task Update(Guid id, string firstName, string name, string email, string passwordHash);
}