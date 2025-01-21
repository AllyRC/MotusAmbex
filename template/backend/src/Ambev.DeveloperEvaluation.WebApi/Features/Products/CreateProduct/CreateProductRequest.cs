namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Request model for creating a product.
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// The product description. Must not be empty.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the product being sold. Must be greater than zero.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product. Must be a positive value.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Indicates whether the product is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
