using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Controllers;
using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoreCodeCamp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CampContext>();
            services.AddScoped<ICampRepository, CampRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddApiVersioning( opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 1); // default version
                opt.ReportApiVersions = true;
                //opt.ApiVersionReader = new UrlSegmentApiVersionReader();    // and then change the route to include version
                opt.ApiVersionReader = ApiVersionReader.Combine(  // combine two methos of versioning
                    new HeaderApiVersionReader("X-version"),
                    new QueryStringApiVersionReader("ver", "version"));
                //opt.ApiVersionReader = new HeaderApiVersionReader("X-version");     // in the header I would add X-version as key adn then the value
                //opt.ApiVersionReader = new QueryStringApiVersionReader("ver");    // ?ver=2.0 instead of ?api-version=2.0

                // this is anohter way to do th versioning instead of having the version on top of a controller
                // they can be centrelised here
                opt.Conventions.Controller<TalksController>()
                    .HasApiVersion(new ApiVersion(1, 0))
                    .HasApiVersion(new ApiVersion(1, 1))
                    .Action(c => c.Delete(default(string), default(int)))
                    .MapToApiVersion(1,1); // the delete is applicable only in the 1.1 version
            });

            // opt => opt.EnableEndpointRouting = false is required for net core 2.2
            services.AddMvc(opt => opt.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
