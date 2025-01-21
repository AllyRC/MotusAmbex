using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.CustomerName)
                .NotEmpty()
                .WithMessage("The customer name must not be empty.")
                .MaximumLength(100)
                .WithMessage("The customer name must not exceed 100 characters.");

            RuleFor(sale => sale.Branch)
                .NotEmpty()
                .WithMessage("The branch must not be empty.")
                .MaximumLength(50)
                .WithMessage("The branch name must not exceed 50 characters.");

            RuleFor(sale => sale.Products)
                .NotEmpty()
                .WithMessage("The sale must include at least one product.")
                .ForEach(productRule => productRule.SetValidator(new CreateProductCommandValidator()))
                .WithMessage("All products in the sale must be valid.");
        }
    }   
}
