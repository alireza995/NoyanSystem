using Application.Customers.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Basic.Pagination;
using Domain.Repository;
using FluentValidation;
using MediatR;

namespace Application.Customers;

public class List
{
    public class Query : IRequest<Paginator<CustomerDto>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? SearchText { get; init; }
    }

    public class CommandValidator : AbstractValidator<Query>
    {
        public CommandValidator()
        {
            RuleFor(i => i.PageNumber).GreaterThan(0).LessThan(1000);
            RuleFor(i => i.PageSize).GreaterThan(9).LessThan(101);
        }
    }

    public class Handler : IRequestHandler<Query, Paginator<CustomerDto>>
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

        public async Task<Paginator<CustomerDto>> Handle(
            Query request,
            CancellationToken ct
        )
        {
            var result = await _customerRepository.Queryable()
               .Where(
                    i => request.SearchText == null ||
                        i.FirstName.Contains(request.SearchText) ||
                        i.LastName.Contains(request.SearchText)
                )
               .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
               .Page(
                    request.PageNumber,
                    request.PageSize,
                    ct
                );

            return result;
        }
    }
}