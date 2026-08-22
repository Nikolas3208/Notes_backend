namespace Notes.API.Contracts.Note;

public record NoteResponce(
    Guid Id,
    Guid OwnerId,
    string Title,
    string Text,
    DateTime Created);