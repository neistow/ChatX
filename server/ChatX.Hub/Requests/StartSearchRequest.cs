
using ChatX.Domain;

namespace ChatX.Hub.Requests;

public record StartSearchRequest
{
    public UserInfo UserInfo { get; init; } = null!;
    public UserInfo SearchPreferences { get; init; } = null!;
}

public record UserInfo(Gender Gender, AgeRange Age);