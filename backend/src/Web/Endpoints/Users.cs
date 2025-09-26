using CleanArchitectureApi.Infrastructure.Identity;

namespace CleanArchitectureApi.Web.Endpoints;

public class Users : EndpointGroupBase
{
    //public override void Map(WebApplication app)
    //{
    //    app.MapGroup(this)
    //        .MapIdentityApi<ApplicationUser>();
    //}
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("")
            .MapIdentityApi<ApplicationUser>();
    }
}
