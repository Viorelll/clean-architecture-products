using CleanArchitectureApi.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureApi.Application.ProductItems.EventHandlers;

public class ProductItemCompletedEventHandler : INotificationHandler<ProductItemCompletedEvent>
{
    private readonly ILogger<ProductItemCompletedEventHandler> _logger;

    public ProductItemCompletedEventHandler(ILogger<ProductItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitectureApi Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
