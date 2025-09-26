using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Application.Common.Security;
using CleanArchitectureApi.Domain.Constants;

namespace CleanArchitectureApi.Application.ProductLists.Commands.PurgeProductLists;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanPurge)]
public record PurgeProductListsCommand : IRequest;

public class PurgeProductListsCommandHandler : IRequestHandler<PurgeProductListsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeProductListsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeProductListsCommand request, CancellationToken cancellationToken)
    {
        _context.ProductLists.RemoveRange(_context.ProductLists);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
