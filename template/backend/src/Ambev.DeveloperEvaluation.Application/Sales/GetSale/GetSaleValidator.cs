using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Validator for GetProductCommand
/// </summary>
public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductCommand
    /// </summary>
    public GetSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
