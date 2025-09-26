﻿using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.ProductLists.Queries.GetProducts;

public class ProductItemDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    public int Priority { get; init; }

    public string? Note { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductItem, ProductItemDto>().ForMember(d => d.Priority, 
                opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}
