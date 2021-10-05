using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bank.Data;
using BankAPI.Controllers.Customer;
using BankAPI.Controllers.Manager;
using BankAPI.Controllers.Officer;
using BankAPI.Controllers.Teller;
using Microsoft.EntityFrameworkCore;

namespace BankAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            /* Here we register our DBContext, which is BankContext, and we are getting the connection string from
             appsettings.json under the parent category of ConnectionStrings.
             */
            services.AddDbContext<BankContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DeveloperDb"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                    .EnableSensitiveDataLogging());
            
            // Here we configure to ignore the infinite loops of relation/reference created by ef core
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // Allows us to use ICustomer/Customer Repository as a service
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IOfficerRepository, OfficerRepository>();
            services.AddScoped<ITellerRepository, TellerRepository>();
            
            // This allows us to use the mapper which can convert Objects to model Objects and vice versa.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
