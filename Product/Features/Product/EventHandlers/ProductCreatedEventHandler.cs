using Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Product.Features.Product.EventHandlers;

internal sealed class ProductCreatedEventHandler : INotificationHandler<DomainEventNotification<ProductCreatedEvent>>
{
    private readonly ILogger<ProductCreatedEvent> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEvent> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<ProductCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("VerticalSlice Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}