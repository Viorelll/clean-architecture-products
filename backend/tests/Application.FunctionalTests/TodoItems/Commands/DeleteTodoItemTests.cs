using CleanArchitectureApi.Application.ProductItems.Commands.CreateProductItem;
using CleanArchitectureApi.Application.ProductItems.Commands.DeleteProductItem;
using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductItems.Commands;

using static Testing;

public class DeleteProductItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductItemId()
    {
        var command = new DeleteProductItemCommand(99);

        await Should.ThrowAsync<NotFoundException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldDeleteProductItem()
    {
        var listId = await SendAsync(new CreateProductListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateProductItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await SendAsync(new DeleteProductItemCommand(itemId));

        var item = await FindAsync<ProductItem>(itemId);

        item.ShouldBeNull();
    }
}
