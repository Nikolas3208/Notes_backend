namespace Notes.API.Contracts;

public record UserResponce(
    Guid Id,
    string FirstName,
    string Name,
    string Email);