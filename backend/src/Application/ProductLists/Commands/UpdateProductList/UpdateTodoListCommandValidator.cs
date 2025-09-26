using CleanArchitectureApi.Application.Common.Interfaces;

namespace CleanArchitectureApi.Application.ProductLists.Commands.UpdateProductList;

public class UpdateProductListCommandValidator : AbstractValidator<UpdateProductListCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductListCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(UpdateProductListCommand model, string title, CancellationToken cancellationToken)
    {
        return !await _context.ProductLists
            .Where(l => l.Id != model.Id)
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}
