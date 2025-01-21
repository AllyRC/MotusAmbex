using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}
