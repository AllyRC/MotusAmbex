using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesValidator : AbstractValidator<GetSalesCommand>
    {
        public GetSalesValidator()
        {
            RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page Size must be greater than or equal to 1");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page Number must be greater than or equal to 1");
        }
    }
}
