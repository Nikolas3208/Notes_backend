namespace Notes.API.Contracts;

public record UserRequest(
    string FirstName,
    string Name,
    string Email,
    string Password);