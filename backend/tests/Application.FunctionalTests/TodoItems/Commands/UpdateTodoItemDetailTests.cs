using CleanArchitectureApi.Application.ProductItems.Commands.CreateProductItem;
using CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItem;
using CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItemDetail;
using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Domain.Entities;
using CleanArchitectureApi.Domain.Enums;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductItems.Commands;

using static Testing;

public class UpdateProductItemDetailTests : BaseTestFixture
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

        var command = new UpdateProductItemDetailCommand
        {
            Id = itemId,
            ListId = listId,
            Note = "This is the note.",
            Priority = PriorityLevel.High
        };

        await SendAsync(command);

        var item = await FindAsync<ProductItem>(itemId);

        item.ShouldNotBeNull();
        item!.ListId.ShouldBe(command.ListId);
        item.Note.ShouldBe(command.Note);
        item.Priority.ShouldBe(command.Priority);
        item.LastModifiedBy.ShouldNotBeNull();
        item.LastModifiedBy.ShouldBe(userId);
        item.LastModified.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
