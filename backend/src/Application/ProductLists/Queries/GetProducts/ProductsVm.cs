using CleanArchitectureApi.Application.Common.Models;

namespace CleanArchitectureApi.Application.ProductLists.Queries.GetProducts;

public class ProductsVm
{
    public IReadOnlyCollection<LookupDto> PriorityLevels { get; init; } = Array.Empty<LookupDto>();

    public IReadOnlyCollection<ProductListDto> Lists { get; init; } = Array.Empty<ProductListDto>();
}
