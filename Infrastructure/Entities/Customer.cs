using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class Customer
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string CompanyRegistrationNumber { get; set; }

        //Date added
        [Required]
        public DateTime IncorporationDate { get; set; }

        //added new
        public decimal Turnover { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}