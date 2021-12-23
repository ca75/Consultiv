using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InMemoryDbContext context;

        public CustomerRepository(InMemoryDbContext context)
        {
            this.context = context;
        }

        public int Add(Customer customer)
        {
            this.context.Add(customer);

            return this.context.SaveChanges(); ;
        }

        //new edit
        public int Edit(Customer customer)
        {
            var cust = GetById(customer.CustomerId);
            cust.Name = customer.Name;
            cust.CompanyRegistrationNumber = customer.CompanyRegistrationNumber;
            cust.IncorporationDate = customer.IncorporationDate;
            cust.Turnover = customer.Turnover;
            cust.IsActive = customer.IsActive;
            this.context.Update(cust);
            return this.context.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            //changed the retrieval
            return this.context.Customer.Where(c => c.IsActive == true).OrderBy(c => c.Name).ToList();
        }

        //new get by id
        public Customer GetById(int id)
        {

            return this.context.Customer.Where(c => c.CustomerId == id).FirstOrDefault();
        }

        //new search
        public List<Customer> Search(string searchTerm)
        {
            var customers = context.Customer.Where(c => c.IsActive == true & c.Name.ToLower().Contains(searchTerm.ToLower()) |
            c.CompanyRegistrationNumber.ToLower().Contains(searchTerm.ToLower())).OrderBy(c => c.Name).ToList();
            return customers;
        }

    }
}