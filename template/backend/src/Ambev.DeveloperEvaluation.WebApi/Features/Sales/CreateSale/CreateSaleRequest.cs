using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale transaction in the system.
/// </summary>
public class CreateSaleRequest
{

    /// <summary>
    /// Gets or sets the name of the customer making the purchase.
    /// Must not be null or empty.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of product IDs included in the sale.
    /// </summary>
    public List<CreateProductRequest> Products { get; set; } = new List<CreateProductRequest>();
}
