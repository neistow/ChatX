using ChatX.Application.Events;
using ChatX.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace ChatX.Application.Commands;

public record SendTypingCommand(string SenderId) : IRequest;

public class SendTypingCommandHandler : AsyncRequestHandler<SendTypingCommand>
{
    private readonly IDatabase _redisDatabase;
    private readonly IMediator _mediator;

    public SendTypingCommandHandler(IDatabase redisDatabase, IMediator mediator)
    {
        _redisDatabase = redisDatabase;
        _mediator = mediator;
    }

    protected override async Task Handle(SendTypingCommand request, CancellationToken cancellationToken)
    {
        var senderId = request.SenderId;

        var conversationId = await _redisDatabase.StringGetAsync(RedisKeys.UserConversation(senderId));
        if (conversationId.IsNullOrEmpty)
        {
            return;
        }

        await _mediator.Publish(new UserTypingEvent(senderId, conversationId!));
    }
}