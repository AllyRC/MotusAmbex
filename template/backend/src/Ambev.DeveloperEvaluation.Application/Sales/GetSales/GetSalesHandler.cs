using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSalesHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetSalesValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var salesQuery = _saleRepository.GetSaleAsQueryable(request.SaleNumber, cancellationToken);

            var count = await salesQuery.CountAsync();
            var items = await salesQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            GetSalesResult getSalesResult = new GetSalesResult();
            getSalesResult.Items = _mapper.Map<List<GetSaleResult>>(items);
            getSalesResult.TotalRecords = count;

            return getSalesResult;
        }
    }
}
