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

    public async Task<List<User>> Get()
    {
        var usersEntity = await _context.Users
            .AsNoTracking()
            .ToListAsync();

        var notes = usersEntity
            .Select(u => User.Create(u.Id, u.FirstName, u.Name, u.Email, u.PasswordHash).Item2)
            .ToList();

        return notes;
    }

    public async Task<Guid> Create(Guid id, string firstName, string name, string email, string passwordHash)
    {
        var userEntity = new UserEntity(id, firstName, name, email, passwordHash);

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        return userEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string firstName, string name, string email, string passwordHash)
    {
        await _context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.FirstName, u => firstName)
                .SetProperty(u => u.Name, u => name)
                .SetProperty(u => u.Email, u => email)
                .SetProperty(u => u.PasswordHash, u => passwordHash));

        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}