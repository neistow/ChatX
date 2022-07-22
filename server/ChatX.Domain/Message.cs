namespace ChatX.Domain;

public class Message
{
    public string SenderId { get; }
    public string Text { get; }
    public DateTime DateTimeSent { get; }

    public Message(string senderId, string text)
    {
        SenderId = senderId;
        Text = text;
        DateTimeSent = DateTime.UtcNow;
    }
}