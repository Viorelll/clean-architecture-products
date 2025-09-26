using CleanArchitectureApi.Application.ProductItems.Commands.CreateProductItem;
using CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItem;
using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductItems.Commands;

using static Testing;

public class UpdateProductItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductItemId()
    {
        var command = new UpdateProductItemCommand { Id = 99, Title = "New Title" };
        await Should.ThrowAsync<NotFoundException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldUpdateProductItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateProductListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateProductItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new UpdateProductItemCommand
        {
            Id = itemId,
            Title = "Updated Item Title"
        };

        await SendAsync(command);

        var item = await FindAsync<ProductItem>(itemId);

        item.ShouldNotBeNull();
        item!.Title.ShouldBe(command.Title);
        item.LastModifiedBy.ShouldNotBeNull();
        item.LastModifiedBy.ShouldBe(userId);
        item.LastModified.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
