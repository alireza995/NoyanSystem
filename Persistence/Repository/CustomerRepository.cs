using Basic.Context;
using Basic.Repository;
using Domain;
using Domain.Repository;

namespace Persistence.Repository;

public class CustomerRepository
    : GenericRepository<Customer, int>,
        ICustomerRepository
{
    public CustomerRepository(
        IDataContext dataContext,
        IGenericRepositoryHandler genericRepositoryHandler
    ) : base(dataContext, genericRepositoryHandler)
    {
    }
}