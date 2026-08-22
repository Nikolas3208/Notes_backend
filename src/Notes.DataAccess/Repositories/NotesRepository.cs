using Microsoft.EntityFrameworkCore;
using Notes.Core.Abstractions;
using Notes.Core.Models;
using Notes.DataAccess.Entities;

namespace Notes.DataAccess.Repositories;

public class NotesRepository : INotesRepository
{
    private readonly NotesDbContext _context;
    
    public NotesRepository(NotesDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Note>> Get()
    {
        var notesEntity = await _context.Notes
            .AsNoTracking()
            .ToListAsync();

        var notes = notesEntity
            .Select(n => Note.Create(n.Id, n.OwnerId, n.Title, n.Text, n.Created).Item2)
            .ToList();

        return notes;
    }

    public async Task<List<Note>> Get(Guid ownerId)
    {
        var notesEntity = await _context.Notes
            .Where(n => n.OwnerId == ownerId)
            .AsNoTracking()
            .ToListAsync();

        var notes = notesEntity
            .Select(n => Note.Create(n.Id, n.OwnerId, n.Title, n.Text, n.Created).Item2)
            .ToList();

        return notes;
    }

    public async Task<Guid> Create(Guid id, Guid ownerId, string title, string text, DateTime created)
    {
        var noteEntity = new NoteEntity(id, ownerId, title, text, created);

        await _context.Notes.AddAsync(noteEntity);
        await _context.SaveChangesAsync();

        return noteEntity.Id;
    }

    public async Task<Guid> Update(Guid id, Guid ownerId, string title, string text, DateTime created)
    {
        await _context.Notes
            .Where(n => n.Id == id)
            .ExecuteUpdateAsync(note => note
                .SetProperty(n => n.OwnerId, n => ownerId)
                .SetProperty(n => n.Title, n => title)
                .SetProperty(n => n.Text, n => text)
                .SetProperty(n => n.Created, n => n.Created));   
        
        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Notes
            .Where(n => n.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}