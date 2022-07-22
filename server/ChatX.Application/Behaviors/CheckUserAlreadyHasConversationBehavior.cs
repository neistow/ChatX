using ChatX.Application.Commands;
using ChatX.Domain;
using ChatX.Infrastructure.Data;
using MediatR;

namespace ChatX.Application.Behaviors;

public class CheckUserAlreadyHasConversationBehavior : IPipelineBehavior<StartSearchCommand, Conversation?>
{
    private readonly ChatDbContext _chatDbContext;

    public CheckUserAlreadyHasConversationBehavior(ChatDbContext chatDbContext)
    {
        _chatDbContext = chatDbContext;
    }

    public async Task<Conversation?> Handle(
        StartSearchCommand request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<Conversation?> next)
    {
        var user = request.User;
        var userMatching = await _chatDbContext.Users.FindAsync(user.Id);
        if (userMatching != null)
        {
            return null;
        }

        return await next();
    }
}