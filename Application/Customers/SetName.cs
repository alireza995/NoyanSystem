using Application.Customers.Exceptions;
using Domain.Repository;
using FluentValidation;
using MediatR;

namespace Application.Customers;

public class SetName
{
    public class Command : IRequest
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(i => i.FirstName).MinimumLength(3);
            RuleFor(i => i.LastName).MinimumLength(3);
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
            _customerRepository.Update(
                request.Id,
                i => i.FirstName,
                request.FirstName
            );
            _customerRepository.Update(
                request.Id,
                i => i.LastName,
                request.LastName
            );

            if (await _customerRepository.SaveChangesAsync(ct))
            {
                return;
            }

            throw new UpdateCustomerFailed(request.Id);
        }
    }
}