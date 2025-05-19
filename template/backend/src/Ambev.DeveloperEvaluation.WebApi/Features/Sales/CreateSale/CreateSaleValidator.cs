using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.");

        RuleFor(sale => sale.SaleDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.Customer)
            .NotEmpty().WithMessage("Customer is required.");

        RuleFor(sale => sale.Branch)
            .NotEmpty().WithMessage("Branch is required.");

        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Total sale amount must be non-negative.");

        RuleForEach(sale => sale.SalesItem)
            .SetValidator(new CreateSaleItemValidator());

        RuleFor(sale => sale.SalesItem)
            .NotEmpty().WithMessage("At least one item must be included in the sale.");
    }
}
public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemValidator()
    {
        RuleFor(item => item.Product)
            .NotEmpty().WithMessage("Product is required.");

        RuleFor(item => item.Quantity)
            .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");

        RuleFor(item => item.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative.");

        RuleFor(item => item.Discount)
            .Must((item, discount) =>
            {
                if (item.Quantity < 4)
                    return discount == 0;
                if (item.Quantity < 10)
                    return discount == 0.10m;
                return discount == 0.20m;
            })
            .WithMessage(item => $"Invalid discount for quantity {item.Quantity}.")
            .When(item => item.Quantity > 0);

        RuleFor(item => item.TotalAmount)
            .Must((item, total) =>
            {
                var expected = item.Quantity * item.UnitPrice * (1 - item.Discount);
                return Math.Round(expected, 2) == Math.Round(item.TotalAmount, 2);
            })
            .WithMessage("Total amount does not match calculated value.");
    }
}