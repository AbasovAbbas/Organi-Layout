using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrganiWebUI.appCode.Providers;
using Organi.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebUI
{
    public class Startup
    {
        readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddRouting(opt =>
            {
                opt.LowercaseUrls = true;
                opt.LowercaseQueryStrings = true;
            });

            services.AddDbContext<OrganiDbContext>(cfg =>
            {
                //cfg.UseInMemoryDatabase(databaseName : "Organi");
                cfg.UseSqlServer(Configuration.GetConnectionString("cString"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Seed();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedCultures("en", "az", "ru");
                cfg.AddSupportedUICultures("en", "az", "ru");
                cfg.RequestCultureProviders.Clear(); // Clears all the default culture providers from the list
                cfg.RequestCultureProviders.Add(new AppCultureProvider());
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "AreaAdmin",
                    areaName: "Admin",
                    pattern: "{lang=az}/Admin/{controller}/{action}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{lang=az}/{controller=home}/{action=index}/{id:int:min(1)?}"// eyni  
                   /* constraints : new
                    {
                        id = new IRouteConstraint[]
                        {
                            new IntRouteConstraint(),
                            new MinRouteConstraint(1)
                        }
                    }*/
                    );
            });
        }
    }
}
