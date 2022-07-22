using ChatX.Domain;
using MediatR;

namespace ChatX.Application.Events;

public record ConversationCreatedEvent(Conversation Conversation) : INotification;