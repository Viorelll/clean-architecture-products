namespace CleanArchitectureApi.Application.ProductItems.Commands.CreateProductItem;

public class CreateProductItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    public CreateProductItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
