using Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace User.Features.Group.EventHandlers;

internal sealed class GroupCreatedEventHandler : INotificationHandler<DomainEventNotification<GroupCreatedEvent>>
{
    private readonly ILogger<GroupCreatedEvent> _logger;

    public GroupCreatedEventHandler(ILogger<GroupCreatedEvent> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<GroupCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("VerticalSlice Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}