using System;

namespace Common.Models.Requests.Customer
{
    public class AddCustomerRequest
    {
        public string Name { get; set; }

        public string CompanyRegistrationNumber { get; set; }

        //new date added
        public DateTime IncorporationDate { get; set; }

        //turnover added
        public decimal Turnover { get; set; }

        public bool IsActive { get; set; }
    }

    //new class added to edit with
    public class EditCustomerRequest
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string CompanyRegistrationNumber { get; set; }


        public DateTime IncorporationDate { get; set; }

        public decimal Turnover { get; set; }

        public bool IsActive { get; set; }
    }
}