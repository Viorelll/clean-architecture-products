using CleanArchitectureApi.Application.Common.Interfaces;

namespace CleanArchitectureApi.Application.ProductLists.Commands.DeleteProductList;

public record DeleteProductListCommand(int Id) : IRequest;

public class DeleteProductListCommandHandler : IRequestHandler<DeleteProductListCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.ProductLists.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
