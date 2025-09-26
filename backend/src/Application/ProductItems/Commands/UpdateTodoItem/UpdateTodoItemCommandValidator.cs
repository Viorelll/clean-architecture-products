namespace CleanArchitectureApi.Application.ProductItems.Commands.UpdateProductItem;

public class UpdateProductItemCommandValidator : AbstractValidator<UpdateProductItemCommand>
{
    public UpdateProductItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
