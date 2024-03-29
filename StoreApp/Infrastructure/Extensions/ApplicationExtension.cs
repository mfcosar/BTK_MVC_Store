﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        { //Webapplication da extend edilebilirdi, interface extended.
            RepositoryContext context = app
                .ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RepositoryContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate(); // dotnet ef database update ==> Bu komuta ihtiyaç kalmadı
            }

        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR")
                .AddSupportedUICultures("tr-TR")
                .SetDefaultCulture("tr-TR");
            });
        }

        public static async void ConfigureDefaultAdminUser( this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin+123456";

            //UserManager
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            //RoleManager: Admin'e bütün rolleri vermek istediğimiz için role manager tanımlandı
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);

            if (user is null)
            { //kullanıcının varlığı kontrol ediir, yoksa oluşturulur
                user = new IdentityUser()
                {
                    Email = "btkakademi@btk.gov.tr",
                    PhoneNumber = "5057055555",
                    UserName = adminUser
                };
                var result = await userManager.CreateAsync(user, adminPassword);

                if (!result.Succeeded)
                    throw new Exception("Admin user cannot be formed");

                // Kullanıcıya rolleri atanır
                var roleResult = await userManager.AddToRolesAsync(user,
                   roleManager.Roles.Select(r => r.Name).ToList()
                    );

                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role definition of admin");

            }

        }


    }
}
