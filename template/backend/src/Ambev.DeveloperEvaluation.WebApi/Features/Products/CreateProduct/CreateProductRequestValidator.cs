using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes validation rules for CreateProductRequest.
    /// </summary>
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Unit price must be a positive value.");
    }
}
