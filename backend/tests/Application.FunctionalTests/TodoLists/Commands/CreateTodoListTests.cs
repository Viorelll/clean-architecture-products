using CleanArchitectureApi.Application.Common.Exceptions;
using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductLists.Commands;

using static Testing;

public class CreateProductListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProductListCommand();
        await Should.ThrowAsync<ValidationException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateProductListCommand
        {
            Title = "Shopping"
        });

        var command = new CreateProductListCommand
        {
            Title = "Shopping"
        };

        await Should.ThrowAsync<ValidationException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldCreateProductList()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateProductListCommand
        {
            Title = "Tasks"
        };

        var id = await SendAsync(command);

        var list = await FindAsync<ProductList>(id);

        list.ShouldNotBeNull();
        list!.Title.ShouldBe(command.Title);
        list.CreatedBy.ShouldBe(userId);
        list.Created.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
