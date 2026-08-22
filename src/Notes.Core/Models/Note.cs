namespace Notes.Core.Models;

public class Note
{
    public const int MaxTitleLength = 50;
    
    public Guid Id { get; }
    
    public Guid OwnerId { get; }

    public string Title { get; }

    public string Text { get; }

    public DateTime Created { get; }

    private Note(Guid id, Guid ownerId, string title, string text, DateTime created)
    {
        Id = id;
        OwnerId = ownerId;
        Title = title;
        Text = text;
        Created = created;
    }

    public static (string, Note) Create(Guid id, Guid ownerId, string title, string text, DateTime created)
    {
        string error = string.Empty;

        if (string.IsNullOrEmpty(title) || title.Length > MaxTitleLength)
        {
            error = $"The title is empty or the length is greater than {MaxTitleLength}";
        }

        return (error, new Note(id, ownerId, title, text, created));
    }
}