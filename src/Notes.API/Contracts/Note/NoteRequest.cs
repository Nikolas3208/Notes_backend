namespace Notes.API.Contracts.Note;

public record NoteRequest(
    string Title,
    string Text);