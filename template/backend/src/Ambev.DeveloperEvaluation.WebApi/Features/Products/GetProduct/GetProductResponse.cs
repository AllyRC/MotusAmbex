using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// API response model for GetProduct operation.
/// </summary>
public class GetProductResponse
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to the product.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total amount for this product after applying discounts.
    /// </summary>
    public decimal TotalAmount => (UnitPrice * Quantity) * (1 - Discount);

    /// <summary>
    /// Indicates whether the product is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
