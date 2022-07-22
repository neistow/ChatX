using ChatX.Application.Events;
using ChatX.Domain;
using ChatX.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatX.Application.Commands;

public record StartSearchCommand(User User) : IRequest<Conversation?>;

public class StartSearchCommandHandler : IRequestHandler<StartSearchCommand, Conversation?>
{
    private readonly ChatDbContext _chatDbContext;
    private readonly IMediator _mediator;

    public StartSearchCommandHandler(ChatDbContext chatDbContext, IMediator mediator)
    {
        _chatDbContext = chatDbContext;
        _mediator = mediator;
    }

    public async Task<Conversation?> Handle(StartSearchCommand request, CancellationToken cancellationToken)
    {
        var userToMatch = request.User;

        var match = await _chatDbContext.Users
            .OrderBy(u => u.SearchStartedAt)
            .Where(u => userToMatch.PreferredGenders.HasFlag(u.Gender) && userToMatch.PreferredAges.HasFlag(u.Age) &&
                        u.PreferredGenders.HasFlag(userToMatch.Gender) && u.PreferredAges.HasFlag(userToMatch.Age))
            .FirstOrDefaultAsync(cancellationToken);
        if (match == null)
        {
            _chatDbContext.Users.Add(userToMatch);
            await _chatDbContext.SaveChangesAsync(cancellationToken);
            await _mediator.Publish(new UserEnqueuedForMatchingEvent());
            return null;
        }

        var conversation = new Conversation(userToMatch.Id, match.Id);
        _chatDbContext.Conversations.Add(conversation);
        _chatDbContext.Users.Remove(match);

        await _chatDbContext.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new ConversationCreatedEvent(conversation));
        await _mediator.Publish(new UserDequeuedFromMatchingEvent());

        return conversation;
    }
}