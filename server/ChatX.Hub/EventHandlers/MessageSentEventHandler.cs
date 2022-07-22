using ChatX.Application.Events;
using ChatX.Hub.Hubs;
using ChatX.Hub.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.EventHandlers;

public class MessageSentEventHandler : INotificationHandler<MessageSentEvent>
{
    private readonly IHubContext<ChatHub, IChatHub> _chatHubContext;

    public MessageSentEventHandler(IHubContext<ChatHub, IChatHub> chatHubContext)
    {
        _chatHubContext = chatHubContext;
    }

    public async Task Handle(MessageSentEvent notification, CancellationToken cancellationToken)
    {
        var (groupName, message) = notification;
        var messageModel = new MessageModel(message.Text, message.DateTimeSent);

        await _chatHubContext.Clients
            .GroupExcept(groupName, message.SenderId)
            .MessageReceived(messageModel);
    }
}