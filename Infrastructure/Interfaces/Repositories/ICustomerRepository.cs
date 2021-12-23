using Infrastructure.Entities;
using System.Collections.Generic;

namespace Infrastructure.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        int Add(Customer customer);

        //new
        int Edit(Customer customer);
              
        List<Customer> GetAll();

        //new
        List<Customer> Search(string searchTerm);

        //new
        Customer GetById(int id);
    }
}