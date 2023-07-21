using Application.Customers.Dto;
using Application.Customers.Exceptions;
using AutoMapper;
using Domain;
using Domain.Repository;
using FluentValidation;
using MediatR;

namespace Application.Customers;

public class Add
{
    public class Command : IRequest<Customer>
    {
        public AddCustomerDto Customer { get; init; } = null!;
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(i => i.Customer.FirstName).MinimumLength(3);
            RuleFor(i => i.Customer.LastName).MinimumLength(3);
            RuleFor(i => i.Customer.Mobile).Matches("^09\\d{9}$");
            RuleFor(i => i.Customer.NationalCode).GreaterThan(99999999).LessThan(100000000000);
            RuleFor(i => i.Customer.CertificateNumber).GreaterThan(0).LessThan(1000000000);
            RuleFor(i => i.Customer.Address).MaximumLength(1000);
        }
    }

    public class Handler : IRequestHandler<Command, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public Handler(
            ICustomerRepository customerRepository,
            IMapper mapper
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Customer> Handle(
            Command request,
            CancellationToken ct
        )
        {
            var result = await _customerRepository.Add(_mapper.Map<Customer>(request.Customer), ct);

            if (await _customerRepository.SaveChangesAsync(ct))
            {
                return result.Entity;
            }

            throw new AddCustomerFailure();
        }
    }
}