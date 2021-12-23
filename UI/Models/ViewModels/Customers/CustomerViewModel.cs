using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace UI.Models.ViewModels.Customers
{
//updated and add two more
    public class CustomerViewModel
    {
        [Display(Name = "Id")]
        public int CustomerId { get; set; }


        public string Name { get; set; }

        [Display(Name = "Reg No.")]
        public string CompanyRegistrationNumber { get; set; }

        [Display(Name = "Incorp Date")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IncorporationDate { get; set; }

        public decimal Turnover { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }

    public class AddCustomerViewModel
{
[Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Registratio No.")]
        public string CompanyRegistrationNumber { get; set; }

        [Display(Name = "Incorporation Date")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? IncorporationDate { get; set; }

        public decimal? Turnover { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
}

    public class EditCustomerViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Reg No.")]
        public string CompanyRegistrationNumber { get; set; }

        [Display(Name = "Incorp Date")]
        public DateTime? IncorporationDate { get; set; }

        public decimal? Turnover { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}