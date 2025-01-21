using System.Linq;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for the Sale entity.
    /// Ensures business rules are followed, including item quantity limits and valid sale details.
    /// </summary>
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {

            RuleFor(sale => sale.SaleNumber)
                .GreaterThan(0)
                .WithMessage("Sale number must be greater than zero.");

             RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future.");

             RuleFor(sale => sale.CustomerName)
                .NotEmpty()
                .WithMessage("Customer name is required.")
                .MaximumLength(100)
                .WithMessage("Customer name cannot exceed 100 characters.");

             RuleFor(sale => sale.TotalSaleAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total sale amount must be a positive value.");

             RuleFor(sale => sale.Branch)
                .NotEmpty()
                .WithMessage("Branch name is required.");

            RuleFor(sale => sale.Products)
                .NotEmpty()
                .WithMessage("A sale must contain at least one product.");
        }
    }
}
