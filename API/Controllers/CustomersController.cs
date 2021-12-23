using Common.Models.Requests.Customer;
using Common.Models.Responses.Customer;
using Infrastructure.Entities;
using Infrastructure.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        //updated
        [HttpGet()]
        public IActionResult GetAll()
        {
            List<Customer> customers = this.customerRepository.GetAll();


            CustomerListResponse customerList = new CustomerListResponse
            {
                Customers = new List<CustomerResponse>(),
            };

            foreach (Customer c in customers)
            {
                customerList.Customers.Add(new CustomerResponse
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    CompanyRegistrationNumber = c.CompanyRegistrationNumber,
                    IncorporationDate = c.IncorporationDate,
                    Turnover = c.Turnover,
                    IsActive = c.IsActive,
                });
            }

            return this.Ok(customerList);
        }

        //Search added
        [HttpGet("{searchTerm}")]        
        public IActionResult Search(string searchTerm)
        {
            List<Customer> customers = this.customerRepository.Search(searchTerm);

            CustomerListResponse customerList = new CustomerListResponse
            {
                Customers = new List<CustomerResponse>(),
            };

            foreach (Customer c in customers)
            {
                customerList.Customers.Add(new CustomerResponse
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    CompanyRegistrationNumber = c.CompanyRegistrationNumber,
                    IncorporationDate = c.IncorporationDate,
                    Turnover = c.Turnover,
                    IsActive = c.IsActive,
                });
            }

            return this.Ok(customerList);
        }

        // getcustomer by id
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Customer customer = this.customerRepository.GetById(id);

            CustomerResponse CustomerResponse = new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                IsActive = customer.IsActive,
                CompanyRegistrationNumber = customer.CompanyRegistrationNumber,
                IncorporationDate = customer.IncorporationDate,
                Turnover = customer.Turnover
            };

            return this.Ok(customer);
        }

        //Edit an individual customer
        [HttpPost("{id}")]
        public IActionResult Edit([FromBody] EditCustomerRequest editCustomerRequest)
        {
            Customer customer = new Customer
            {
                CustomerId = editCustomerRequest.CustomerId,
                Name = editCustomerRequest.Name,
                CompanyRegistrationNumber = editCustomerRequest.CompanyRegistrationNumber,
                IncorporationDate = editCustomerRequest.IncorporationDate,
                Turnover = editCustomerRequest.Turnover,
                IsActive = editCustomerRequest.IsActive,
            };

            int rowsAffected = this.customerRepository.Edit(customer);

            if (rowsAffected == 0)
            {
                return this.BadRequest("Add failed.");
            }

            return this.Ok();
        }


        //updated
        [HttpPost]
        public IActionResult Add([FromBody] AddCustomerRequest addCustomerRequest)
        {
            Customer customer = new Customer
            {
                Name = addCustomerRequest.Name,
                CompanyRegistrationNumber = addCustomerRequest.CompanyRegistrationNumber,
                IncorporationDate = addCustomerRequest.IncorporationDate,
                Turnover = addCustomerRequest.Turnover,
                IsActive = addCustomerRequest.IsActive

            };

            int rowsAffected = this.customerRepository.Add(customer);

            if (rowsAffected == 0)
            {
                return this.BadRequest("Add failed.");
            }

            return this.Ok();
        }
    }
}