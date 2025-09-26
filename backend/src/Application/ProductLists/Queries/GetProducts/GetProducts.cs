using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Application.Common.Models;
using CleanArchitectureApi.Application.Common.Security;
using CleanArchitectureApi.Domain.Enums;

namespace CleanArchitectureApi.Application.ProductLists.Queries.GetProducts;

[Authorize]
public record GetProductsQuery : IRequest<ProductsVm>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductsVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return new ProductsVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),

            Lists = await _context.ProductLists
                .AsNoTracking()
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}
