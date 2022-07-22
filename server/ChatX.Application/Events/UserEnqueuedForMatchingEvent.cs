using MediatR;

namespace ChatX.Application.Events;

public record UserEnqueuedForMatchingEvent : INotification;