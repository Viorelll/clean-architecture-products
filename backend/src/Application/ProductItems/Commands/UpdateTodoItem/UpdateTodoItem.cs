using CleanArchitectureApi.Application.Common.Interfaces;

namespace CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItem;

public record UpdateProductItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateProductItemCommandHandler : IRequestHandler<UpdateProductItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
