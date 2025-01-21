namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Request model for getting a sale by ID
/// </summary>
public class GetSalesRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int? SaleNumber { get; set; }
}
