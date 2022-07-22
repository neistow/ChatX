using ChatX.Application.Commands;
using ChatX.Domain;
using ChatX.Hub.Models;
using ChatX.Hub.Requests;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatX.Hub.Hubs;

public interface IChatHub
{
    Task ConversationStarted();
    Task ConversationEnded();
    Task MessageReceived(MessageModel messageModel);
    Task StrangerTyping();
}

public class ChatHub : Hub<IChatHub>
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task StartSearch(StartSearchRequest request)
    {
        var user = User.Create(
            Context.ConnectionId,
            request.UserInfo.Gender,
            request.UserInfo.Age,
            request.SearchPreferences.Gender,
            request.SearchPreferences.Age
        );
        var command = new StartSearchCommand(user);
        await _mediator.Send(command, Context.ConnectionAborted);
    }

    public async Task StopSearch()
    {
        var command = new StopSearchCommand(Context.ConnectionId);
        await _mediator.Send(command);
    }

    public async Task CancelActiveConversation()
    {
        var command = new CancelActiveConversationCommand(Context.ConnectionId);
        await _mediator.Send(command);
    }

    public async Task SendMessage(SendMessageRequest request)
    {
        var command = new SendMessageCommand(Context.ConnectionId, request.Text);
        await _mediator.Send(command);
    }

    public async Task SendTyping()
    {
        var command = new SendTypingCommand(Context.ConnectionId);
        await _mediator.Send(command);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await StopSearch();
        await CancelActiveConversation();

        await base.OnDisconnectedAsync(exception);
    }
}