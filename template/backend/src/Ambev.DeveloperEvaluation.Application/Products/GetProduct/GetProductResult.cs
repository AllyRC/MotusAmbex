using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Response model for GetProduct operation
/// </summary>
public class GetProductResult
{
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalAmount => (UnitPrice * Quantity) * (1 - Discount);

    public bool IsCancelled { get; set; }
}
