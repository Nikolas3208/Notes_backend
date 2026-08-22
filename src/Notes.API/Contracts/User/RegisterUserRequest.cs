namespace Notes.API.Contracts.User;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);