using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Application.ProductLists.Commands.DeleteProductList;
using CleanArchitectureApi.Application.ProductLists.Commands.UpdateProductList;
using CleanArchitectureApi.Application.ProductLists.Queries.GetProducts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitectureApi.Web.Endpoints;

public class ProductLists : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetProductLists).RequireAuthorization();
        groupBuilder.MapPost(CreateProductList).RequireAuthorization();
        groupBuilder.MapPut(UpdateProductList, "{id}").RequireAuthorization();
        groupBuilder.MapDelete(DeleteProductList, "{id}").RequireAuthorization();
    }

    public async Task<Ok<ProductsVm>> GetProductLists(ISender sender)
    {
        var vm = await sender.Send(new GetProductsQuery());

        return TypedResults.Ok(vm);
    }

    public async Task<Created<int>> CreateProductList(ISender sender, CreateProductListCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(ProductLists)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateProductList(ISender sender, int id, UpdateProductListCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteProductList(ISender sender, int id)
    {
        await sender.Send(new DeleteProductListCommand(id));

        return TypedResults.NoContent();
    }
}
