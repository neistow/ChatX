using ChatX.Application.Events;
using ChatX.Hub.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.EventHandlers;

public class ConversationEndedEventHandler : INotificationHandler<ConversationEndedEvent>
{
    private readonly IHubContext<ChatHub, IChatHub> _chatHubContext;

    public ConversationEndedEventHandler(IHubContext<ChatHub, IChatHub> chatHubContext)
    {
        _chatHubContext = chatHubContext;
    }

    public async Task Handle(ConversationEndedEvent notification, CancellationToken cancellationToken)
    {
        var conversation = notification.Conversation;
        var groupName = conversation.Id;

        await _chatHubContext.Clients.Group(groupName).ConversationEnded();

        await _chatHubContext.Groups.RemoveFromGroupAsync(conversation.UserOneId, groupName);
        await _chatHubContext.Groups.RemoveFromGroupAsync(conversation.UserTwoId, groupName);
    }
}