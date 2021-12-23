using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data to seed
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
                new Customer() { CustomerId = 1, Name = "Tony Smith", CompanyRegistrationNumber = "56789dg", IncorporationDate = System.DateTime.Now, Turnover = 669.1m, IsActive = true },
                new Customer() { CustomerId = 2, Name = "Jane Morgan", CompanyRegistrationNumber = "345768yh", IncorporationDate = System.DateTime.Now.AddDays(-568), Turnover = 56766.1m, IsActive = true },
                new Customer() { CustomerId = 3, Name = "Maria Reaves", CompanyRegistrationNumber = "345678jn", IncorporationDate = System.DateTime.Now.AddDays(-468), Turnover = 636.1m, IsActive = true },
                new Customer() { CustomerId = 4, Name = "Sue Plough", CompanyRegistrationNumber = "7654stgf", IncorporationDate = System.DateTime.Now.AddDays(-678), Turnover = 65666.1m, IsActive = true },
                new Customer() { CustomerId = 6, Name = "Becky Clough", CompanyRegistrationNumber = "67347823f", IncorporationDate = System.DateTime.Now.AddDays(-168), Turnover = 656.1m, IsActive = true },
                new Customer() { CustomerId = 5, Name = "Kim Deane", CompanyRegistrationNumber = "9897654dfh", IncorporationDate = System.DateTime.Now.AddDays(-568), Turnover = 266.1m, IsActive = false }
                );
        }
    }
}