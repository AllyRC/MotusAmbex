using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSalesResponse
{
    public Guid Id { get; set; }

    public int SaleNumber { get; set; }

    public DateTime SaleDate { get; set; }

    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalSaleAmount { get; set; }
    public string Branch { get; set; } = string.Empty;

    public List<GetProductResponse> Products { get; set; } = new List<GetProductResponse>();
}
