using Microsoft.EntityFrameworkCore;
using Notes.DataAccess.Entities;

namespace Notes.DataAccess;

public class NotesDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<NoteEntity> Notes { get; set; }
    
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
    {
        
    }
}