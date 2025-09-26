using CleanArchitectureApi.Application.ProductLists.Queries.GetProducts;
using CleanArchitectureApi.Domain.Entities;
using CleanArchitectureApi.Domain.ValueObjects;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductLists.Queries;

using static Testing;

public class GetProductsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        var query = new GetProductsQuery();

        var result = await SendAsync(query);

        result.PriorityLevels.ShouldNotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new ProductList
        {
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
                {
                    new ProductItem { Title = "Apples", Done = true },
                    new ProductItem { Title = "Milk", Done = true },
                    new ProductItem { Title = "Bread", Done = true },
                    new ProductItem { Title = "Toilet paper" },
                    new ProductItem { Title = "Pasta" },
                    new ProductItem { Title = "Tissues" },
                    new ProductItem { Title = "Tuna" }
                }
        });

        var query = new GetProductsQuery();

        var result = await SendAsync(query);

        result.Lists.Count.ShouldBe(1);
        result.Lists.First().Items.Count.ShouldBe(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetProductsQuery();

        var action = () => SendAsync(query);

        await Should.ThrowAsync<UnauthorizedAccessException>(action);
    }
}
