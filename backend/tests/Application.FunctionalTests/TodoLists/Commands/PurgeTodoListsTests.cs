using CleanArchitectureApi.Application.Common.Exceptions;
using CleanArchitectureApi.Application.Common.Security;
using CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;
using CleanArchitectureApi.Application.ProductLists.Commands.PurgeProductLists;
using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.FunctionalTests.ProductLists.Commands;

using static Testing;

public class PurgeProductListsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var command = new PurgeProductListsCommand();

        command.GetType().ShouldSatisfyAllConditions(
            type => type.ShouldBeDecoratedWith<AuthorizeAttribute>()
        );

        var action = () => SendAsync(command);

        await Should.ThrowAsync<UnauthorizedAccessException>(action);
    }

    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        var command = new PurgeProductListsCommand();

        var action = () => SendAsync(command);

        await Should.ThrowAsync<ForbiddenAccessException>(action);
    }

    [Test]
    public async Task ShouldAllowAdministrator()
    {
        await RunAsAdministratorAsync();

        var command = new PurgeProductListsCommand();

        var action = () => SendAsync(command);

        Func<Task> asyncAction = async () => await SendAsync(command);
        await asyncAction.ShouldNotThrowAsync();
    }

    [Test]
    public async Task ShouldDeleteAllLists()
    {
        await RunAsAdministratorAsync();

        await SendAsync(new CreateProductListCommand
        {
            Title = "New List #1"
        });

        await SendAsync(new CreateProductListCommand
        {
            Title = "New List #2"
        });

        await SendAsync(new CreateProductListCommand
        {
            Title = "New List #3"
        });

        await SendAsync(new PurgeProductListsCommand());

        var count = await CountAsync<ProductList>();

        count.ShouldBe(0);
    }
}
