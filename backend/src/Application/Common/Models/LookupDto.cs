using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductList, LookupDto>();
            CreateMap<ProductItem, LookupDto>();
        }
    }
}
