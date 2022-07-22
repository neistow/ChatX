namespace ChatX.Domain;

public class Conversation
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string UserOneId { get; }
    public string UserTwoId { get; }

    public Conversation(string userOneId, string userTwoId)
    {
        UserOneId = userOneId;
        UserTwoId = userTwoId;
    }
}