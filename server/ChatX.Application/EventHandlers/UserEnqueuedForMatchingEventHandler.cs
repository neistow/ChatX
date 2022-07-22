using ChatX.Application.Events;
using ChatX.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace ChatX.Application.EventHandlers;

public class UserEnqueuedForMatchingEventHandler : INotificationHandler<UserEnqueuedForMatchingEvent>
{
    private readonly IDatabase _redisDatabase;
    private readonly IMediator _mediator;

    public UserEnqueuedForMatchingEventHandler(IDatabase redisDatabase, IMediator mediator)
    {
        _redisDatabase = redisDatabase;
        _mediator = mediator;
    }

    public async Task Handle(UserEnqueuedForMatchingEvent notification, CancellationToken cancellationToken)
    {
        await _redisDatabase.StringIncrementAsync(RedisKeys.UsersInSearchCounter, flags: CommandFlags.FireAndForget);
        await _mediator.Publish(new ChatStatisticsChangedEvent());
    }
}