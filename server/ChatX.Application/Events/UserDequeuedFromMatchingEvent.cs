using MediatR;

namespace ChatX.Application.Events;

public record UserDequeuedFromMatchingEvent : INotification;