using Microsoft.EntityFrameworkCore;
using Notes.DataAccess.Entities;

namespace Notes.DataAccess;

public class NotesDbContext : DbContext
{
    public DbSet<UserEntity> Users;
    public DbSet<NoteEntity> Notes;
    
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
    {
        
    }
}