using System;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product in a sale transaction.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Product : BaseEntity
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

        public Sale Sale { get; set; }
        public Guid SaleId { get; set; }


        /// <summary>
        /// Gets the date and time when the product record was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the product information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the Product class.
        /// </summary>
        public Product()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Performs validation of the product entity using the ProductValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new ProductValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Updates the product details.
        /// </summary>
        /// <param name="description">The product description.</param>
        /// <param name="quantity">The product quantity.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <param name="discount">The discount applied.</param>
        /// <param name="isCancelled">Whether the product is cancelled.</param>
        public void UpdateProduct(string description, int quantity, decimal unitPrice, decimal discount, bool isCancelled)
        {
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            IsCancelled = isCancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
