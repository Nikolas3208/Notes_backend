using Notes.Core.Models;

namespace Notes.Core.Abstractions;

public interface INotesService
{
    Task<List<Note>> Get();

    Task<List<Note>> Get(Guid ownerId);

    Task<Guid> Create(Note note);

    Task<Guid> Update(Guid id, Guid ownerId, string title, string text);

    Task<Guid> Delete(Guid id, Guid ownerId);
}