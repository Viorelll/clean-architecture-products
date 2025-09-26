using CleanArchitectureApi.Application.Common.Interfaces;

namespace CleanArchitectureApi.Application.ProductLists.Commands.UpdateProductList;

public record UpdateProductListCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateProductListCommandHandler : IRequestHandler<UpdateProductListCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductLists
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
