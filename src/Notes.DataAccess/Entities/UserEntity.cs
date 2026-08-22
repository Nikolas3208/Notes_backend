namespace Notes.DataAccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; } = Guid.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserEntity()
    {
        
    }

    public UserEntity(Guid id, string firstName, string name, string email, string passwordHash)
    {
        Id = id;
        FirstName = firstName;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
}