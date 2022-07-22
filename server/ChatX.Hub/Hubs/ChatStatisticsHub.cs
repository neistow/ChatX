using ChatX.Application.Queries;
using ChatX.Hub.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.Hubs;

public interface IChatStatisticsHub
{
    Task ChatStatsUpdated(ChatStatisticsModel statisticsModel);
}

public class ChatStatisticsHub : Hub<IChatStatisticsHub>
{
    private readonly IMediator _mediator;

    public ChatStatisticsHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        var chatStats = await _mediator.Send(new GetChatStatisticsQuery(), Context.ConnectionAborted);
        var model = new ChatStatisticsModel(chatStats.UsersInSearch, chatStats.UsersChatting);

        await Clients.Caller.ChatStatsUpdated(model);

        await base.OnConnectedAsync();
    }
}