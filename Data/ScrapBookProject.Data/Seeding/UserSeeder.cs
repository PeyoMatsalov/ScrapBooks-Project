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
            await SeedUser(dbContext);
        }

        private static async Task SeedUser(ApplicationDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "Peyo.IT@mail.com",
                    NormalizedUserName = "PEYO.IT@MAIL.COM",
                    NormalizedEmail = "PEYO.IT@MAIL.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEMKmC4NqhTWffn4j/f5vLkC2Jb+FWa6R2vqZRm24rHYvJmyfIIZEqCNv48FFe40BnQ==",
                    Email = "Peyo.IT@mail.com",
                    EmailConfirmed = false,
                    SecurityStamp = "RIMWBDMW2JSUOQJCDSSAE6JXFESSDJZ7",
                    ConcurrencyStamp = "cfba750a-df34-46af-8d9e-0baae263f45a",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    CreatedOn = DateTime.Parse("2020-11-23"),
                    IsDeleted = false,
                    DateOfBirth = DateTime.Parse("1996-11-21"),
                };

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
