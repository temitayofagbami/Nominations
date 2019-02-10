using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IndividualNominationCatalogAPI.Data;

namespace IndividualNominationCatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //i added host variable, removed the run funtion from host builder
            //did this to first have db seed first before running the service is kicked of at Main
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context =

                      services.GetRequiredService<IndividualNominationCatalogContext>();

                // IndividualNominationCatalogSeed.SeedAsync(context).Wait();
            }

            //db has been seeded, now run
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

