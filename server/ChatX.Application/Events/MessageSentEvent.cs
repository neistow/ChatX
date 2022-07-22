using ChatX.Domain;
using MediatR;

namespace ChatX.Application.Events;

public record MessageSentEvent(string ConversationId, Message Message) : INotification;
