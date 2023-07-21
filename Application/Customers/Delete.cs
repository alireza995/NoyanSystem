using Application.Customers.Exceptions;
using Domain.Repository;
using MediatR;

namespace Application.Customers;

public class Delete
{
    public class Command : IRequest
    {
        public int Id { get; init; }
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
            _customerRepository.Delete(request.Id);

            if (await _customerRepository.SaveChangesAsync(ct))
            {
                return;
            }

            throw new DeleteCustomerFailed(request.Id);
        }
    }
}