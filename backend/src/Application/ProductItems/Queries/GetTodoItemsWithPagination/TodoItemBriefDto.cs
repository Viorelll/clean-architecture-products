using CleanArchitectureApi.Domain.Entities;
using CleanArchitectureApi.Domain.Enums;

namespace CleanArchitectureApi.Application.ProductItems.Queries.GetProductItemsWithPagination;

public class ProductItemBriefDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    public string? Note { get; set; }

    public string? ImageUrl { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductItem, ProductItemBriefDto>();
        }
    }
}
