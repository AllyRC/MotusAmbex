using Ambev.DeveloperEvaluation.Application.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class CreateProductRequestProfile : Profile
    {
        public CreateProductRequestProfile()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<CreateProductResult, CreateProductResponse>();
        }
    }
}
