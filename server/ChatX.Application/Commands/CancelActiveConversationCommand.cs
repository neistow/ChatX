using ChatX.Application.Events;
using ChatX.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatX.Application.Commands;

public record CancelActiveConversationCommand(string UserId) : IRequest;

public class CancelActiveConversationCommandHandler : AsyncRequestHandler<CancelActiveConversationCommand>
{
    private readonly ChatDbContext _chatDbContext;
    private readonly IMediator _mediator;

    public CancelActiveConversationCommandHandler(ChatDbContext chatDbContext, IMediator mediator)
    {
        _chatDbContext = chatDbContext;
        _mediator = mediator;
    }

    protected override async Task Handle(CancelActiveConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _chatDbContext.Conversations.FirstOrDefaultAsync(
            cr => cr.UserOneId == request.UserId || cr.UserTwoId == request.UserId);
        if (conversation == null)
        {
            return;
        }

        _chatDbContext.Conversations.Remove(conversation);
        await _chatDbContext.SaveChangesAsync();
        await _mediator.Publish(new ConversationEndedEvent(conversation));
    }
}