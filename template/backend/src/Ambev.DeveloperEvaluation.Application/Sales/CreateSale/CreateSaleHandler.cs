using Ambev.DeveloperEvaluation.Application.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleReposiory;
        private readonly IMapper _mapper;
        public CreateSaleHandler(ISaleRepository productReposiory, IMapper mapper)
        {
            _saleReposiory = productReposiory;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Domain.Entities.Sale>(command);

            sale.SaleNumber = await _saleReposiory.GetSaleNumberAsync();
            sale.SaleDate = DateTime.Now;

            foreach (var product in sale.Products)
            {
                decimal discount = 0;
                if (product.Quantity > 4)
                {
                    discount = 0.1M;
                    if(product.Quantity >= 10 && product.Quantity < 20)
                    {
                        discount = 0.2M;
                    }
                }

                product.Discount = discount;
            }

            sale.TotalSaleAmount = sale.Products.Where(p => !p.IsCancelled).Sum(p => p.TotalAmount);

            var createdSale = await _saleReposiory.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
