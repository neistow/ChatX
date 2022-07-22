namespace ChatX.Infrastructure.Redis;

public static class RedisKeys
{
    public const string UsersInSearchCounter = "counter:users.searching";
    public const string UsersChattingCounter = "coutner:users.chatting";
    
    public static string UserConversation(string userId) => $"users:{userId}:conversation";
}
