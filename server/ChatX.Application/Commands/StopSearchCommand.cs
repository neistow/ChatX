using ChatX.Application.Events;
using ChatX.Infrastructure.Data;
using MediatR;

namespace ChatX.Application.Commands;

public record StopSearchCommand(string UserId) : IRequest;

public class StopUserSearchCommandCommandHandler : AsyncRequestHandler<StopSearchCommand>
{
    private readonly ChatDbContext _chatDbContext;
    private readonly IMediator _mediator;

    public StopUserSearchCommandCommandHandler(ChatDbContext chatDbContext, IMediator mediator)
    {
        _chatDbContext = chatDbContext;
        _mediator = mediator;
    }

    protected override async Task Handle(StopSearchCommand request, CancellationToken cancellationToken)
    {
        var user = await _chatDbContext.Users.FindAsync(request.UserId);
        if (user == null)
        {
            return;
        }

        _chatDbContext.Users.Remove(user);
        await _chatDbContext.SaveChangesAsync();
        await _mediator.Publish(new UserDequeuedFromMatchingEvent());
    }
}