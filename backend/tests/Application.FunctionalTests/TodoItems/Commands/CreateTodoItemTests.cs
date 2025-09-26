using CleanArchitectureApi.Application.Common.Exceptions;
using CleanArchitectureApi.Application.ProductItems.Commands.CreateProductItem;
using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductItems.Commands;

using static Testing;

public class CreateProductItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProductItemCommand();

        await Should.ThrowAsync<ValidationException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldCreateProductItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateProductListCommand
        {
            Title = "New List"
        });

        var command = new CreateProductItemCommand
        {
            ListId = listId,
            Title = "Tasks"
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<ProductItem>(itemId);

        item.ShouldNotBeNull();
        item!.ListId.ShouldBe(command.ListId);
        item.Title.ShouldBe(command.Title);
        item.CreatedBy.ShouldBe(userId);
        item.Created.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.ShouldBe(userId);
        item.LastModified.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
