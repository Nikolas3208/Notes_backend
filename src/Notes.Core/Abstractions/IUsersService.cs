using Notes.Core.Models;

namespace Notes.Core.Abstractions;

public interface IUsersService
{
    Task<string> Login(string email, string password);
    
    Task<string> Register(string firstName, string lastName, string email, string password);

    Task Update(Guid id, string firstName, string name, string email, string passwordHash);
}