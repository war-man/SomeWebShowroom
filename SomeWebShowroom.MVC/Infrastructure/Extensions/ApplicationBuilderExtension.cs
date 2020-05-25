using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SomeWebShowroom.MVC.Data;
using SomeWebShowroom.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SomeWebShowroom.MVC.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseDatasaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<SomeWebShowroomDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = WebConstants.AdminRole;

                        var roles = new[]
                        {
                            adminName,
                            WebConstants.UserRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExist = await roleManager.RoleExistsAsync(role);

                            if (!roleExist)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminMail = "admin@some.com";

                        var adminUser = await userManager.FindByNameAsync(adminMail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = adminMail,
                                UserName = "admin"
                            };

                            await userManager.CreateAsync(adminUser, "222222");

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }

                    })
                    .Wait();


                return app;
            }
        }
    }
}
