using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Domain.Entities;
using CleanArchitectureApi.Domain.Events;

namespace CleanArchitectureApi.Application.ProductItems.Commands.CreateProductItem;

public record CreateProductItemCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateProductItemCommandHandler : IRequestHandler<CreateProductItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        entity.AddDomainEvent(new ProductItemCreatedEvent(entity));

        _context.ProductItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
