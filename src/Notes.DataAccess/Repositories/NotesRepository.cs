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

    public async Task<Guid> Create(Note note)
    {
        var noteEntity = new NoteEntity(note.Id, note.OwnerId, note.Title, note.Text, note.Created);

        await _context.Notes.AddAsync(noteEntity);
        await _context.SaveChangesAsync();

        return noteEntity.Id;
    }

    public async Task<Guid> Update(Guid id, Guid ownerId, string title, string text)
    {
        await _context.Notes
            .Where(n => n.Id == id && n.OwnerId == ownerId)
            .ExecuteUpdateAsync(note => note
                .SetProperty(n => n.Title, n => title)
                .SetProperty(n => n.Text, n => text));
        
        return id;
    }

    public async Task<Guid> Delete(Guid id, Guid ownerId)
    {
        await _context.Notes
            .Where(n => n.Id == id && n.OwnerId == ownerId)
            .ExecuteDeleteAsync();

        return id;
    }
}