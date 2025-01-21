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

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _productReposiory;
        private readonly IMapper _mapper;
        public CreateProductHandler(IProductRepository productReposiory,IMapper mapper)
        {
            _productReposiory = productReposiory;
            _mapper = mapper;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            //var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            //if (existingUser != null)
            //    throw new InvalidOperationException($"User with email {command.Email} already exists");

            var user = _mapper.Map<Domain.Entities.Product>(command);

            var createdUser = await _productReposiory.CreateAsync(user, cancellationToken);
            var result = _mapper.Map<CreateProductResult>(createdUser);
            return result;
        }
    }
}
