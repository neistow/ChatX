using ChatX.Application.Events;
using ChatX.Hub.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.EventHandlers;

public class UserTypingEventHandler : INotificationHandler<UserTypingEvent>
{
    private readonly IHubContext<ChatHub, IChatHub> _chatHubContext;

    public UserTypingEventHandler(IHubContext<ChatHub, IChatHub> chatHubContext)
    {
        _chatHubContext = chatHubContext;
    }

    public async Task Handle(UserTypingEvent notification, CancellationToken cancellationToken)
    {
        var (sender, groupName) = notification;

        await _chatHubContext.Clients
            .GroupExcept(groupName, sender)
            .StrangerTyping();
    }
}