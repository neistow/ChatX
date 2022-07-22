using ChatX.Application.Events;
using ChatX.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace ChatX.Application.EventHandlers;

public class ConversationCreatedEventHandler : INotificationHandler<ConversationCreatedEvent>
{
    private readonly IDatabase _redisDatabase;
    private readonly IMediator _mediator;

    public ConversationCreatedEventHandler(IDatabase redisDatabase, IMediator mediator)
    {
        _redisDatabase = redisDatabase;
        _mediator = mediator;
    }

    public async Task Handle(ConversationCreatedEvent notification, CancellationToken cancellationToken)
    {
        var conversation = notification.Conversation;

        var transaction = _redisDatabase.CreateTransaction();

        _ = transaction.StringIncrementAsync(RedisKeys.UsersChattingCounter, 2L);
        _ = transaction.StringSetAsync(RedisKeys.UserConversation(conversation.UserOneId), conversation.Id);
        _ = transaction.StringSetAsync(RedisKeys.UserConversation(conversation.UserTwoId), conversation.Id);

        await transaction.ExecuteAsync(CommandFlags.FireAndForget);

        await _mediator.Publish(new ChatStatisticsChangedEvent());
    }
}