namespace Notes.API.Contracts.User;

public record UserResponce(
    Guid Id,
    string FirstName,
    string Name,
    string Email);