using FluentValidation;
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}