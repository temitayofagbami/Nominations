using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IndividualNominationCatalogAPI
{
    //Startup.cs- when API service starts,  Startup.cs get triggered first
    //i need to set up what is needed for the API to run successfully
    public class Startup
    {

        //startup ctor kicks off the API service by loading service configurations

        //steps for this:
        //appsetting.json contains connections strings needed by API service
        //configuration grabs connection strings from appsettings.json
        //configuration is inject in startup ctor
        //startup ctor -reads the configurations
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //database

            //get connection string from configuration

            var dbConnectionString = Configuration["ConnectionString"];


            //build db by adding DBContext of type  IndividualNominationCatalogContext
            //pass the db connectionstring from configuration to options
            services.AddDbContext<IndividualNominationCatalogContext>(options => options.UseSqlServer(dbConnectionString));



            //cannot call seed here, async issue, db might not be ready to take seed
            //call in Progam.cs in main, it runs after startup


            // Add framework services.
            //add swagger
            services.AddSwaggerGen(options =>

            {

                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v1",

                    new Swashbuckle.AspNetCore.Swagger.Info

                    {

                        Title = "RewardsRecognition - Individual Nomination Catalog HTTP API",

                        Version = "v1",

                        Description = "The Individual Nomination Microservice HTTP API. This is a Data-Driven/CRUD microservice API",

                        TermsOfService = "Terms Of Service"

                    });



            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //configure swagger
            app.UseSwagger()

               .UseSwaggerUI(c =>

               {

                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Individaul Nomination API V1");

               });

            app.UseMvc();
        }
    }
}
