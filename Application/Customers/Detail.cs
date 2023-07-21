using Application.Customers.Dto;
using Application.Customers.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers;

public class Detail
{
    public class Query : IRequest<CustomerDto>
    {
        public int Id { get; init; }
    }

    public class Handler : IRequestHandler<Query, CustomerDto>
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

        public async Task<CustomerDto> Handle(
            Query request,
            CancellationToken ct
        )
        {
            var result = await _customerRepository.Queryable()
               .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(i => i.Id == request.Id, ct);

            if (result is null)
            {
                throw new CustomerNotFound(request.Id);
            }

            return result;
        }
    }
}