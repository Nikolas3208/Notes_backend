namespace Notes.API.Contracts;

public record NoteResponce(
    Guid Id,
    Guid OwnerId,
    string Title,
    string Text,
    DateTime Created);