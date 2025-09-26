using CleanArchitectureApi.Application.Common.Models;
using CleanArchitectureApi.Application.ProductItems.Commands.CreateProductItem;
using CleanArchitectureApi.Application.ProductItems.Commands.DeleteProductItem;
using CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItem;
using CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItemDetail;
using CleanArchitectureApi.Application.ProductItems.Queries.GetProductItemsWithPagination;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitectureApi.Web.Endpoints;

public class ProductItems : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetProductItemsWithPagination).RequireAuthorization();
        groupBuilder.MapPost(CreateProductItem).RequireAuthorization();
        groupBuilder.MapPut(UpdateProductItem, "{id}").RequireAuthorization();
        groupBuilder.MapPut(UpdateProductItemDetail, "UpdateDetail/{id}").RequireAuthorization();
        groupBuilder.MapDelete(DeleteProductItem, "{id}").RequireAuthorization();
    }

    public async Task<Ok<PaginatedList<ProductItemBriefDto>>> GetProductItemsWithPagination(ISender sender, [AsParameters] GetProductItemsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateProductItem(ISender sender, CreateProductItemCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(ProductItems)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateProductItem(ISender sender, int id, UpdateProductItemCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<Results<NoContent, BadRequest>> UpdateProductItemDetail(ISender sender, int id, UpdateProductItemDetailCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteProductItem(ISender sender, int id)
    {
        await sender.Send(new DeleteProductItemCommand(id));

        return TypedResults.NoContent();
    }
}
