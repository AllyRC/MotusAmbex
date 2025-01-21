using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    public Guid Id { get; set; }

    public int SaleNumber { get; set; }

    public DateTime SaleDate { get; set; }

    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalSaleAmount { get; set; }
    public string Branch { get; set; } = string.Empty;

    public List<GetProductResult> Products { get; set; } = new List<GetProductResult>();

}
