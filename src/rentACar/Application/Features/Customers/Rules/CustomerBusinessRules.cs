using Application.Features.Customers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Customers.Rules;

public class CustomerBusinessRules : BaseBusinessRules
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBusinessRules(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CustomerIdShouldExist(int id)
    {
        Customer? result = await _customerRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(CustomersMessages.CustomerNotExists);
    }

    public Task CustomerShouldBeExist(Customer? customer)
    {
        if (customer is null)
            throw new BusinessException(CustomersMessages.CustomerNotExists);
        return Task.CompletedTask;
    }
}
