using ChatX.Application.Events;
using ChatX.Application.Queries;
using ChatX.Hub.Hubs;
using ChatX.Hub.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.EventHandlers;

public class ChatStatisticsChangedEventHandler : INotificationHandler<ChatStatisticsChangedEvent>
{
    private readonly IHubContext<ChatStatisticsHub, IChatStatisticsHub> _statsHubContext;
    private readonly IMediator _mediator;

    public ChatStatisticsChangedEventHandler(IHubContext<ChatStatisticsHub, IChatStatisticsHub> statsHubContext, IMediator mediator)
    {
        _statsHubContext = statsHubContext;
        _mediator = mediator;
    }

    public async Task Handle(ChatStatisticsChangedEvent notification, CancellationToken cancellationToken)
    {
        var chatStats = await _mediator.Send(new GetChatStatisticsQuery());
        var model = new ChatStatisticsModel(chatStats.UsersInSearch, chatStats.UsersChatting);
        await _statsHubContext.Clients.All.ChatStatsUpdated(model);
    }
}