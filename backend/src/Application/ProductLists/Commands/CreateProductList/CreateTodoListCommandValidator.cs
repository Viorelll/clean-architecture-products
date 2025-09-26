using CleanArchitectureApi.Application.Common.Interfaces;

namespace CleanArchitectureApi.Application.ProductLists.Commands.CreateProductList;

public class CreateProductListCommandValidator : AbstractValidator<CreateProductListCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductListCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return !await _context.ProductLists
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}
