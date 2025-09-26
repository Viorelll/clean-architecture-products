using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Domain.Enums;

namespace CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItemDetail;

public record UpdateProductItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateProductItemDetailCommandHandler : IRequestHandler<UpdateProductItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
