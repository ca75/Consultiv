using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //change to add data in db
            var h = CreateHostBuilder(args).Build();
            using (var sc = h.Services.CreateScope())
            {
                var serv = sc.ServiceProvider;
                try
                {
                    var ctx = serv.GetRequiredService<InMemoryDbContext>();
                    ctx.Database.EnsureCreated();
                }
                catch (System.Exception )
                {

                    throw;
                }
            }
            h.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}