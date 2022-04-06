using IntroSignalR.Models.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroSignalR.Models.DataContext
{
    static public class ChatSeedData
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                if(roleManager.FindByNameAsync("Admin").Result== null)
                {
                    var result = roleManager.CreateAsync(new AppRole
                    {
                        Name = "Admin"
                    }).Result;

                    if (result.Succeeded)
                    {
                        if (userManager.FindByEmailAsync("abbas_abasov@list.ru").Result == null)            
                        {
                            var user = new AppUser
                            {
                                UserName = "Abbas",
                                Email = "abbas_abasov@list.ru"
                            };
                            var Aresult = userManager.CreateAsync(user, "murad123").Result; 
                        }
                    }
                }
            }
                return app;
        }
    }
}
