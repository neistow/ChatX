using ChatX.Domain;
using ChatX.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace ChatX.Application.Queries;

public record GetChatStatisticsQuery : IRequest<ChatStats>;

public class GetChatStatisticsQueryHandler : IRequestHandler<GetChatStatisticsQuery, ChatStats>
{
    private readonly IDatabase _redisDatabase;

    public GetChatStatisticsQueryHandler(IDatabase redisDatabase)
    {
        _redisDatabase = redisDatabase;
    }

    public async Task<ChatStats> Handle(GetChatStatisticsQuery request, CancellationToken cancellationToken)
    {
        var chatStats = await _redisDatabase.StringGetAsync(new []
        {
            new RedisKey(RedisKeys.UsersChattingCounter),
            new RedisKey(RedisKeys.UsersInSearchCounter)
        });

        chatStats[0].TryParse(out long usersChatting);
        chatStats[1].TryParse(out long usersInSearch);

        return new ChatStats
        {
            UsersChatting = usersChatting,
            UsersInSearch = usersInSearch
        };
    }
}