namespace Notes.DataAccess.Entities;

public class NoteEntity
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid OwnerId { get; set; } = Guid.Empty;

    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public NoteEntity()
    {
        
    }
    
    public NoteEntity(Guid id, Guid ownerId, string title, string text, DateTime created)
    {
        Id = id;
        OwnerId = ownerId;
        Title = title;
        Text = text;
        Created = created;
    }
}