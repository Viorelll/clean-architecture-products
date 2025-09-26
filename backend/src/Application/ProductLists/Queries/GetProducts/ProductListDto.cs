using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.ProductLists.Queries.GetProducts;

public class ProductListDto
{
    public ProductListDto()
    {
        Items = Array.Empty<ProductItemDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Colour { get; init; }

    public IReadOnlyCollection<ProductItemDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductList, ProductListDto>();
        }
    }
}
