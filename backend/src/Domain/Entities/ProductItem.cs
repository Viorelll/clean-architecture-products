namespace CleanArchitectureApi.Domain.Entities;

public class ProductItem : BaseAuditableEntity
{
    public int ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public string? ImageUrl { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && !_done)
            {
                AddDomainEvent(new ProductItemCompletedEvent(this));
            }

            _done = value;
        }
    }

    public ProductList List { get; set; } = null!;
}
