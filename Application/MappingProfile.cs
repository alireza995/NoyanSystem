using Application.Customers.Dto;
using AutoMapper;
using Domain;

namespace Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();
        CreateMap<Customer, CustomerDto>();
    }
}