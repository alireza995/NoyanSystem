using Application.Customers.Dto;
using Application.Customers.Exceptions;
using Domain.Repository;
using FluentValidation;
using MediatR;

namespace Application.Customers;

public class Update
{
    public class Command : IRequest
    {
        public int Id { get; init; }
        public UpdateCustomerDto Customer { get; init; } = null!;
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(i => i.Customer.Mobile).Matches("^09\\d{9}$");
            RuleFor(i => i.Customer.NationalCode).GreaterThan(99999999).LessThan(100000000000);
            RuleFor(i => i.Customer.CertificateNumber).GreaterThan(0).LessThan(1000000000);
            RuleFor(i => i.Customer.Address).MaximumLength(1000);
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly ICustomerRepository _customerRepository;

        public Handler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(
            Command request,
            CancellationToken ct
        )
        {
            _customerRepository.Update(request.Id, request.Customer);

            if (await _customerRepository.SaveChangesAsync(ct))
            {
                return;
            }

            throw new UpdateCustomerFailed(request.Id);
        }
    }
}