using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SingleStone.Contact.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // set up Fluent Validation
            services.AddMvc(setup => {
                //...mvc setup...
            }).AddFluentValidation();

            services.AddHttpContextAccessor();

            // add validators.  Would move this if there were more of these
            services.AddTransient<IValidator<Models.Contact>, Validators.ContactValidator>();
            services.AddTransient<IValidator<Models.ContactName>, Validators.ContactNameValidator>();
            services.AddTransient<IValidator<Models.Address>, Validators.AddressValidator>();
            services.AddTransient<IValidator<Models.Phone>, Validators.PhoneValidator>();

            // bind the sendgrid options at startup
            services.Configure<Utilities.ContactOptions>(Configuration.GetSection(Utilities.ContactOptions.Contact));

            StaticConfig = Configuration.GetSection(Utilities.ContactOptions.Contact);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            // set the webroot which we will use later.
            Utilities.ContactOptions.WebRoot = env.ContentRootPath;
        }
    }
}
