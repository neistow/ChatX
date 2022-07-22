using ChatX.Application.Events;
using ChatX.Hub.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.EventHandlers;

public class ConversationCreatedEventHandler : INotificationHandler<ConversationCreatedEvent>
{
    private readonly IHubContext<ChatHub, IChatHub> _chatHubContext;

    public ConversationCreatedEventHandler(IHubContext<ChatHub, IChatHub> chatHubContext)
    {
        _chatHubContext = chatHubContext;
    }

    public async Task Handle(ConversationCreatedEvent notification, CancellationToken cancellationToken)
    {
        var conversation = notification.Conversation;
        var groupName = conversation.Id;

        await _chatHubContext.Groups.AddToGroupAsync(conversation.UserOneId, groupName);
        await _chatHubContext.Groups.AddToGroupAsync(conversation.UserTwoId, groupName);

        await _chatHubContext.Clients.Group(groupName).ConversationStarted();
    }
}