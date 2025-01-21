using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for GetSaleRequest
/// </summary>
public class GetSalesRequestValidator : AbstractValidator<GetSalesRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleRequest
    /// </summary>
    public GetSalesRequestValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page Size must be greater than or equal to 1");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page Number must be greater than or equal to 1");
    }
}
