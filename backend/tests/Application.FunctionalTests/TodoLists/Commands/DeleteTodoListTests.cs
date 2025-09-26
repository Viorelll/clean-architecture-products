using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Application.ProductLists.Commands.DeleteProductList;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductLists.Commands;

using static Testing;

public class DeleteProductListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductListId()
    {
        var command = new DeleteProductListCommand(99);
        await Should.ThrowAsync<NotFoundException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldDeleteProductList()
    {
        var listId = await SendAsync(new CreateProductListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteProductListCommand(listId));

        var list = await FindAsync<ProductList>(listId);

        list.ShouldBeNull();
    }
}
