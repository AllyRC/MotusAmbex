using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public record GetSalesCommand : IRequest<GetSalesResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? SaleNumber { get; set; }

        public GetSalesCommand(int pageNumber, int pageSize, int? saleNumber)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SaleNumber = saleNumber;
        }
    }
}
