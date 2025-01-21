using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Profile for mapping GetSale feature requests to commands
/// </summary>
public class GetSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature
    /// </summary>
    public GetSalesProfile()
    {
        CreateMap<GetSalesRequest, GetSalesCommand>();
        CreateMap<GetSalesResult, GetSalesResponse>();
    }
}
