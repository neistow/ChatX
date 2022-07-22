using ChatX.Domain;
using MediatR;

namespace ChatX.Application.Events;

public record ConversationEndedEvent(Conversation Conversation) : INotification;