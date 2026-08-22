using Microsoft.EntityFrameworkCore;
using Notes.Core.Abstractions;
using Notes.Core.Models;
using Notes.DataAccess.Entities;

namespace Notes.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly NotesDbContext _context;
    
    public UsersRepository(NotesDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (userEntity is null)
            return null;
        
        return User.Create(
            userEntity.Id,
            userEntity.FirstName,
            userEntity.Name,
            userEntity.Email,
            userEntity.PasswordHash).Item2;
    }

    public async Task<string> Add(User user)
    {
        var userEntity = new UserEntity(user.Id, user.FirstName, user.Name, user.Email, user.PasswordHash);

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        return string.Empty;
    }

    public async Task Update(Guid id, string firstName, string name, string email, string passwordHash)
    {
        await _context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.FirstName, u => firstName)
                .SetProperty(u => u.Name, u => name)
                .SetProperty(u => u.Email, u => email)
                .SetProperty(u => u.PasswordHash, u => passwordHash));
    }
}