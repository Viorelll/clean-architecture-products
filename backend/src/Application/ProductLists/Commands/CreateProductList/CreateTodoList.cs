using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;

public record CreateProductListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateProductListCommandHandler : IRequestHandler<CreateProductListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductListCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductList();

        entity.Title = request.Title;

        _context.ProductLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
