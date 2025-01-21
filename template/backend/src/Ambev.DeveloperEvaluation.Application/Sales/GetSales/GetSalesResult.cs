using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesResult
    {
        public List<GetSaleResult> Items { get; set; }
        public int TotalRecords { get; set; }

        public GetSalesResult()
        {
            Items = new List<GetSaleResult>();
        }
    }
}
