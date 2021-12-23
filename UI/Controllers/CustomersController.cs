using Common.Models.Requests.Customer;
using Common.Models.Responses.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UI.Interfaces.Services;
using UI.Models;
using UI.Models.ViewModels.Customers;

namespace UI.Controllers
{
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private IHttpClientService HttpClientService { get; set; }
        private JsonSerializerOptions apiJsonSerializerOptions;

        public CustomersController(IHttpClientService httpClientService)
        {
            this.HttpClientService = httpClientService ?? throw new ArgumentNullException(nameof(httpClientService));
        }

        //updated
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Result = TempData["Result"];
            CustomerListViewModel customerListViewModel = new CustomerListViewModel
            {
                Customers = new List<CustomerViewModel>(),
            };

            APICallResult<CustomerListResponse> apiCallResult =
                await this.HttpClientService.MakeRequest<CustomerListResponse>(HttpMethod.Get,
                "http://localhost:50781/api/customers/")
                .ConfigureAwait(true);

            if (apiCallResult.IsSuccessStatusCode)
            {
                if (apiCallResult.ResultObject.Customers != null && apiCallResult.ResultObject.Customers.Count > 0)
                {
                    foreach (CustomerResponse cr in apiCallResult.ResultObject.Customers)
                    {
                        customerListViewModel.Customers.Add(
                            new CustomerViewModel
                            {
                                CustomerId = cr.CustomerId,
                                Name = cr.Name,
                                CompanyRegistrationNumber = cr.CompanyRegistrationNumber,
                                IncorporationDate = cr.IncorporationDate,
                                Turnover = cr.Turnover,
                                IsActive = cr.IsActive,
                            });
                    }
                }
            }
            else
            {
                TempData["Error"] = $"Error - {apiCallResult.HttpStatusCode.ToString()}";
                // Display error.
            }

            return View(customerListViewModel);
        }

        //new
        [HttpGet("Search")]       
        public async Task<IActionResult> Search(string searchTerm)
        {
            CustomerListViewModel customerListViewModel = new CustomerListViewModel
            {
                Customers = new List<CustomerViewModel>(),
            };


            APICallResult<CustomerListResponse> apiCallResult =
                await this.HttpClientService.MakeRequest<CustomerListResponse>(HttpMethod.Get,
                $"http://localhost:50781/api/customers/{searchTerm}")
                .ConfigureAwait(true);

            if (apiCallResult.IsSuccessStatusCode)
            {
                if (apiCallResult.ResultObject.Customers != null && apiCallResult.ResultObject.Customers.Count > 0)
                {
                    foreach (CustomerResponse cr in apiCallResult.ResultObject.Customers)
                    {
                        customerListViewModel.Customers.Add(
                            new CustomerViewModel
                            {
                                CustomerId = cr.CustomerId,
                                Name = cr.Name,
                                CompanyRegistrationNumber = cr.CompanyRegistrationNumber,
                                IncorporationDate = cr.IncorporationDate,
                                Turnover = cr.Turnover,
                                IsActive = cr.IsActive,
                            });
                    }
                }
            }
            else
            {
                // Display error.
            }

            return View("Index", customerListViewModel);
        }


        //new
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            APICallResult<CustomerResponse> apiCallResult = await this.HttpClientService.MakeRequest<CustomerResponse>(
                   HttpMethod.Get, $"http://localhost:50781/api/customers/GetById/{id}").ConfigureAwait(true);
            var customerViewModel = new EditCustomerViewModel()
            {
                CustomerId = apiCallResult.ResultObject.CustomerId,
                Name = apiCallResult.ResultObject.Name,
                CompanyRegistrationNumber = apiCallResult.ResultObject.CompanyRegistrationNumber,
                IncorporationDate = apiCallResult.ResultObject.IncorporationDate,
                Turnover = apiCallResult.ResultObject.Turnover,
                IsActive = apiCallResult.ResultObject.IsActive
            };

            return View(customerViewModel);
        }

        //new
        [HttpPost("Edit")]
        public async Task<IActionResult> ProcessEdit(EditCustomerViewModel customerViewModel)
        {
            if (customerViewModel.IncorporationDate.ToString() == "01/01/0001 00:00:00" || customerViewModel.IncorporationDate == null)
            {
                ModelState.AddModelError("InCorporationDate", "The Incorporation Date field is required");
            }
          
            if (ModelState.IsValid)
            {
                EditCustomerRequest editCustomerRequest = new EditCustomerRequest
                {
                    CustomerId = customerViewModel.CustomerId,
                    Name = customerViewModel.Name,
                    CompanyRegistrationNumber = customerViewModel.CompanyRegistrationNumber,
                    IncorporationDate = customerViewModel.IncorporationDate.HasValue ? (DateTime)customerViewModel.IncorporationDate : DateTime.Now,
                    Turnover = customerViewModel.Turnover.HasValue? (decimal)customerViewModel.Turnover : 0,
                    IsActive = customerViewModel.IsActive
                };

                APICallResult<CustomerResponse> apiCallResult = await this.HttpClientService.MakeRequest<CustomerResponse>(
                    HttpMethod.Post,
                    $"http://localhost:50781/api/customers/{customerViewModel.CustomerId}",
                    this.Serialize<EditCustomerRequest>(editCustomerRequest)).ConfigureAwait(true);

                if (apiCallResult.IsSuccessStatusCode)
                {
                    TempData["EditResult"] = $"Customer {customerViewModel.Name} updated";
                    return RedirectToAction("index");
                    // Update successful.
                }
                else
                {
                    ModelState.AddModelError("Error", apiCallResult.HttpStatusCode.ToString());
                    return View("~/Views/Customers/Edit.cshtml");
                    // Display error.
                }
            }

            return View("~/Views/Customers/Edit.cshtml");
        }


        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }

        //updated
        [HttpPost("Add")]
        public async Task<IActionResult> ProcessAdd(AddCustomerViewModel customerViewModel)
        {
            if (customerViewModel.IncorporationDate.ToString() == "01/01/0001 00:00:00" || customerViewModel.IncorporationDate == null)
            {
                ModelState.AddModelError("InCorporationDate", "The Incorporation Date field is required");
            }

            if (ModelState.IsValid)
            {
                AddCustomerRequest addCustomerRequest = new AddCustomerRequest
                {
                    Name = customerViewModel.Name,
                    CompanyRegistrationNumber = customerViewModel.CompanyRegistrationNumber,
                    IncorporationDate = customerViewModel.IncorporationDate == null ? DateTime.Now : DateTime.Now,
                    Turnover = (decimal)(customerViewModel.Turnover.HasValue ? customerViewModel.Turnover : 0),
                    IsActive = customerViewModel.IsActive
                };

                APICallResult<CustomerResponse> apiCallResult = await this.HttpClientService.MakeRequest<CustomerResponse>(
                    HttpMethod.Post,
                    "http://localhost:50781/api/customers/",
                    this.Serialize<AddCustomerRequest>(addCustomerRequest)).ConfigureAwait(true);

                if (apiCallResult.IsSuccessStatusCode)
                {
                    TempData["Result"] = $"Customer {customerViewModel.Name} added";
                    return RedirectToAction("index");
                    // Update successful.
                }
                else
                {
                    ModelState.AddModelError("Error", apiCallResult.HttpStatusCode.ToString());
                    return View("~/Views/Customers/Add.cshtml");
                    // Display error.
                }
            }


            return View("~/Views/Customers/Add.cshtml");
        }

        private string Serialize<T>(T objectToBeSerialized)
        {
            return JsonSerializer.Serialize<T>(objectToBeSerialized, this.ApiJsonSerializerOptions);
        }

        protected JsonSerializerOptions ApiJsonSerializerOptions
        {
            get
            {
                if (this.apiJsonSerializerOptions == null)
                {
                    this.apiJsonSerializerOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true,
                        PropertyNameCaseInsensitive = true,
                    };
                }

                return this.apiJsonSerializerOptions;
            }
        }
    }
}