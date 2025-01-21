using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Microsoft.AspNetCore.Identity;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction in the system.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the unique sale number.
        /// Used to identify the sale in the system.
        /// </summary>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Gets the date of the sale transaction.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets the name of the customer making the purchase.
        /// Must not be null or empty.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the total amount of the sale.
        /// Must be a positive value.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Gets the branch where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets the list of products included in the sale.
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Gets the date and time when the sale was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the sale information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the Sale class.
        /// </summary>
        public Sale()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Performs validation of the sale entity using the SaleValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Updates the sale details.
        /// </summary>
        /// <param name="saleNumber">The sale number.</param>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="customerName">The customer name.</param>
        /// <param name="totalSaleAmount">The total sale amount.</param>
        /// <param name="branch">The branch name.</param>
        /// <param name="products">The list of products.</param>
        public void UpdateSale(int saleNumber, DateTime saleDate, string customerName, decimal totalSaleAmount, string branch, List<Product> products)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerName = customerName;
            TotalSaleAmount = totalSaleAmount;
            Branch = branch;
            Products = products;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
