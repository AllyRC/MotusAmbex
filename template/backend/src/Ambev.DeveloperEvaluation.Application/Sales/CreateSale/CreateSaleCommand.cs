using Ambev.DeveloperEvaluation.Application.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets the name of the customer making the purchase.
        /// Must not be null or empty.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the branch where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets the list of products included in the sale.
        /// </summary>
        public List<CreateProductCommand> Products { get; set; } = new List<CreateProductCommand>();

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
