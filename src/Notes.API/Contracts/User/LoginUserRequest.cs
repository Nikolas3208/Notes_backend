namespace Notes.API.Contracts.User;

public record LoginUserRequest(
    string Email,
    string Password);