namespace Notes.API.Contracts;

public record NoteRequest(
    string Title,
    string Text);