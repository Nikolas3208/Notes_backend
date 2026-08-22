namespace Notes.Core.Abstractions;

public interface IJwtProvider
{
    string Generate(Guid userId);
}