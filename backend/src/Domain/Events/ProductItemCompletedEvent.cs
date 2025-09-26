namespace CleanArchitectureApi.Domain.Events;

public class ProductItemCompletedEvent : BaseEvent
{
    public ProductItemCompletedEvent(ProductItem item)
    {
        Item = item;
    }

    public ProductItem Item { get; }
}
