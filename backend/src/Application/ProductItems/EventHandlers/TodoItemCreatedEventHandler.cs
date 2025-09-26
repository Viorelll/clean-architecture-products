using CleanArchitectureApi.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureApi.Application.ProductItems.EventHandlers;

public class ProductItemCreatedEventHandler : INotificationHandler<ProductItemCreatedEvent>
{
    private readonly ILogger<ProductItemCreatedEventHandler> _logger;

    public ProductItemCreatedEventHandler(ILogger<ProductItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitectureApi Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
