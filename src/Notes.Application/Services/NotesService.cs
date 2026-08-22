using Notes.Core.Abstractions;
using Notes.Core.Models;

namespace Notes.Application.Services;

public class NotesService : INotesService
{
    private readonly INotesRepository _notesRepository;

    public NotesService(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }
    
    public async Task<List<Note>> Get()
    {
        return await _notesRepository.Get();
    }

    public async Task<List<Note>> Get(Guid ownerId)
    {
        return await _notesRepository.Get(ownerId);
    }

    public async Task<Guid> Create(Note note)
    {
        return await _notesRepository.Create(note);
    }

    public async Task<Guid> Update(Guid id, Guid ownerId, string title, string text)
    {
        return await _notesRepository.Update(id, ownerId, title, text);
    }

    public async Task<Guid> Delete(Guid id, Guid ownerId)
    {
        return await _notesRepository.Delete(id, ownerId);
    }
}