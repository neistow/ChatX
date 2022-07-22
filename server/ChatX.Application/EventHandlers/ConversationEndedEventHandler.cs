using ChatX.Application.Events;
using ChatX.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace ChatX.Application.EventHandlers;

public class ConversationEndedEventHandler : INotificationHandler<ConversationEndedEvent>
{
    private readonly IDatabase _redisDatabase;
    private readonly IMediator _mediator;

    public ConversationEndedEventHandler(IDatabase redisDatabase, IMediator mediator)
    {
        _redisDatabase = redisDatabase;
        _mediator = mediator;
    }

    public async Task Handle(ConversationEndedEvent notification, CancellationToken cancellationToken)
    {
        var conversation = notification.Conversation;

        var transaction = _redisDatabase.CreateTransaction();

        _ = transaction.KeyExpireAsync(RedisKeys.UserConversation(conversation.UserOneId), TimeSpan.Zero);
        _ = transaction.KeyExpireAsync(RedisKeys.UserConversation(conversation.UserTwoId), TimeSpan.Zero);
        _ = transaction.StringDecrementAsync(RedisKeys.UsersChattingCounter, 2L);

        await transaction.ExecuteAsync(CommandFlags.FireAndForget);

        await _mediator.Publish(new ChatStatisticsChangedEvent());
    }
}