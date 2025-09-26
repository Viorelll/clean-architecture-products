using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Application.Common.Mappings;
using CleanArchitectureApi.Application.Common.Models;

namespace CleanArchitectureApi.Application.ProductItems.Queries.GetProductItemsWithPagination;

public record GetProductItemsWithPaginationQuery : IRequest<PaginatedList<ProductItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProductItemsWithPaginationQueryHandler : IRequestHandler<GetProductItemsWithPaginationQuery, PaginatedList<ProductItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductItemBriefDto>> Handle(GetProductItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProductItems
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .ProjectTo<ProductItemBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
