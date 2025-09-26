using CleanArchitectureApi.Application.Common.Exceptions;
using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Application.ProductLists.Commands.UpdateProductList;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductLists.Commands;

using static Testing;

public class UpdateProductListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductListId()
    {
        var command = new UpdateProductListCommand { Id = 99, Title = "New Title" };
        await Should.ThrowAsync<NotFoundException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await SendAsync(new CreateProductListCommand
        {
            Title = "New List"
        });

        await SendAsync(new CreateProductListCommand
        {
            Title = "Other List"
        });

        var command = new UpdateProductListCommand
        {
            Id = listId,
            Title = "Other List"
        };

        var ex = await Should.ThrowAsync<ValidationException>(() => SendAsync(command));

        ex.Errors.ShouldContainKey("Title");
        ex.Errors["Title"].ShouldContain("'Title' must be unique.");
    }

    [Test]
    public async Task ShouldUpdateProductList()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateProductListCommand
        {
            Title = "New List"
        });

        var command = new UpdateProductListCommand
        {
            Id = listId,
            Title = "Updated List Title"
        };

        await SendAsync(command);

        var list = await FindAsync<ProductList>(listId);

        list.ShouldNotBeNull();
        list!.Title.ShouldBe(command.Title);
        list.LastModifiedBy.ShouldNotBeNull();
        list.LastModifiedBy.ShouldBe(userId);
        list.LastModified.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
