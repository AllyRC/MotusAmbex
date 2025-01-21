using Ambev.DeveloperEvaluation.Application.AuthenticateUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        /// <summary>
        /// Gets the product description.
        /// Must not be null or empty.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets the quantity of the product being sold.
        /// Must be greater than zero.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// Must be a positive value.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the discount applied to the product.
        /// Must be a non-negative value.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets the total amount for this product after applying discounts.
        /// </summary>
        public decimal TotalAmount => (UnitPrice * Quantity) * (1 - Discount);

        /// <summary>
        /// Indicates whether the product is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
