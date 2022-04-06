using IntroSignalR.appCode.Hubs;
using IntroSignalR.appCode.Middlewares;
using IntroSignalR.Models.DataContext;
using IntroSignalR.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroSignalR
{
    public class Startup
    {
        readonly IConfiguration conf;
        public Startup(IConfiguration conf)
        {
            this.conf = conf;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(cfg =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                 cfg.Filters.Add(new AuthorizeFilter(policy));
            });
            
            services.AddSignalR();

            services.AddDbContext<ChatDbContext>(cfg =>
                cfg.UseSqlServer(conf.GetConnectionString("cString")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<ChatDbContext>();

            services.AddScoped<SignInManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>>();
            services.AddScoped<UserManager<AppUser>>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "chat";
                options.AccessDeniedPath = "/accessdenied.html";
                options.LoginPath = "/signin.html";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;

                cfg.Password.RequireDigit = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequireNonAlphanumeric = false;

                cfg.Lockout.AllowedForNewUsers = true;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                cfg.Lockout.MaxFailedAccessAttempts = 3;
            });
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseErrorMiddleware();

            app.Seed();
            app.UseRouting();
            app.UseStaticFiles();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "default",
                    areaName: "admin",
                    pattern: "admin/{controller=home}/{action=index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "default",
                    areaName: "admin",
                    pattern: "signin.html",
                    defaults : new
                    {
                        area = "admin",
                        controller = "account",
                        action = "signin"
                    });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "error.html",
                    defaults: new
                    {
                        controller = "error",
                        action = "index"
                    });
            });
        }
    }
}
