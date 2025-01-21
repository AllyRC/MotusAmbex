using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleNumber: Must be greater than 0
    /// - SaleDate: Must be a valid date in the past or present
    /// - CustomerName: Required, length between 3 and 100 characters
    /// - TotalSaleAmount: Must be positive
    /// - Branch: Required, length between 3 and 50 characters
    /// - ProductIds: Must contain at least one product
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 100);
        RuleFor(sale => sale.Branch).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.Products).NotEmpty().WithMessage("A sale must contain at least one product.");
    }
}
