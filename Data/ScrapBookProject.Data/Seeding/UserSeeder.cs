namespace ScrapBookProject.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using ScrapBookProject.Data.Models;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await SeedUser(dbContext, userManager);
        }

        private static async Task SeedUser(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            if (!dbContext.Users.Any())
            {
                var user = new ApplicationUser { UserName = "Peyo.It@mail.com", Email = "Peyo.It@mail.com", DateOfBirth = DateTime.Parse("11.12.1993") };
                await userManager.CreateAsync(user, "Test1234%");
                for (int i = 0; i < 15; i++)
                {
                    var testUser = new ApplicationUser { UserName = $"User{i}.It@mail.com", Email = $"User{i}.It@mail.com", DateOfBirth = DateTime.Parse("11.12.1966") };
                    await userManager.CreateAsync(testUser, "Test1234%");
                }

                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }
}
