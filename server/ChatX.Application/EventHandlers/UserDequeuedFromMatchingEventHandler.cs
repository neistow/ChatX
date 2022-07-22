using ChatX.Application.Events;
using ChatX.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace ChatX.Application.EventHandlers;

public class UserDequeuedFromMatchingEventHandler : INotificationHandler<UserDequeuedFromMatchingEvent>
{
    private readonly IDatabase _redisDatabase;
    private readonly IMediator _mediator;

    public UserDequeuedFromMatchingEventHandler(IDatabase redisDatabase, IMediator mediator)
    {
        _redisDatabase = redisDatabase;
        _mediator = mediator;
    }

    public async Task Handle(UserDequeuedFromMatchingEvent notification, CancellationToken cancellationToken)
    {
        await _redisDatabase.StringDecrementAsync(RedisKeys.UsersInSearchCounter, flags: CommandFlags.FireAndForget);
        await _mediator.Publish(new ChatStatisticsChangedEvent());
    }
}