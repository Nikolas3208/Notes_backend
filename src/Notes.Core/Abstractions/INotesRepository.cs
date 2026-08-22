using Notes.Core.Models;

namespace Notes.Core.Abstractions;

public interface INotesRepository
{
    Task<List<Note>> Get();

    Task<List<Note>> Get(Guid ownerId);

    Task<Guid> Create(Guid id, Guid ownerId, string title, string text, DateTime created);

    Task<Guid> Update(Guid id, Guid ownerId, string title, string text, DateTime created);

    Task<Guid> Delete(Guid id);
}