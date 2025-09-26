using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Domain.Events;

namespace CleanArchitectureApi.Application.ProductItems.Commands.DeleteProductItem;

public record DeleteProductItemCommand(int Id) : IRequest;

public class DeleteProductItemCommandHandler : IRequestHandler<DeleteProductItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.ProductItems.Remove(entity);

        entity.AddDomainEvent(new ProductItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
