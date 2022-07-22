using ChatX.Application.Events;
using ChatX.Domain;
using ChatX.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace ChatX.Application.Commands;

public record SendMessageCommand(string SenderId, string Text) : IRequest;

public class SendMessageCommandHandler : AsyncRequestHandler<SendMessageCommand>
{
    private readonly IDatabase _redisDatabase;
    private readonly IMediator _mediator;

    public SendMessageCommandHandler(IDatabase redisDatabase, IMediator mediator)
    {
        _redisDatabase = redisDatabase;
        _mediator = mediator;
    }

    protected override async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var (senderId, text) = request;

        var conversationId = await _redisDatabase.StringGetAsync(RedisKeys.UserConversation(senderId));
        if (conversationId.IsNullOrEmpty)
        {
            return;
        }

        var message = new Message(senderId, text);
        await _mediator.Publish(new MessageSentEvent(conversationId.ToString(), message));
    }
}