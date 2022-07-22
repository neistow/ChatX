using MediatR;

namespace ChatX.Application.Events;

public record ChatStatisticsChangedEvent : INotification;
