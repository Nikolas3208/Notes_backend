namespace Notes.API.Contracts.User;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);