namespace Notes.Core.Models;

public class User
{
    public const int MaxFirstNameLength = 50;
    public const int MaxNameLength = 50;
    public const int MaxEmailLength = 50;
    
    public Guid Id { get; }
    
    public string FirstName { get; }
    
    public string Name { get; }
    
    public string Email { get; }
    
    public string PasswordHash { get; }

    private User(Guid id, string firstName, string name, string email, string passwordHash)
    {
        Id = id;
        FirstName = firstName;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static (string, User) Create(Guid id, string firstName, string name, string email, string passwordHash)
    {
        string error = string.Empty;

        if (string.IsNullOrEmpty(firstName) || firstName.Length > MaxFirstNameLength)
        {
            error = $"The firstName is empty or the length is greater than {MaxFirstNameLength}";
        }
        
        if (string.IsNullOrEmpty(name) || firstName.Length > MaxNameLength)
        {
            error = $"The name is empty or the length is greater than {MaxNameLength}";
        }
        
        if (string.IsNullOrEmpty(email) || firstName.Length > MaxEmailLength)
        {
            error = $"The email is empty or the length is greater than {MaxEmailLength}";
        }

        if (string.IsNullOrEmpty(passwordHash))
        {
            error = "The passwordHash is empty";
        }

        return (error, new User(id, firstName, name, email, passwordHash));
    }
}