using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Quantity)
                .LessThanOrEqualTo(20)
                .WithMessage("Não é possível vender acima de 20 itens idênticos.");

            RuleFor(product => product.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Unit price must be a positive value.");

            RuleFor(product => product.Discount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount must be a non-negative value.");
        }
    }
}
