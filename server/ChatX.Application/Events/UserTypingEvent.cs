using MediatR;

namespace ChatX.Application.Events;

public record UserTypingEvent(string UserId, string ConversationId) : INotification;