namespace ScrapBookProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using Xunit;

    public class StatisticsServiceTests
    {
        [Fact]
        public void GetRegisteredUserCountForPast3MonthsShouldWorkCorrectly()
        {
            var listUser = new List<ApplicationUser>()
            {
                new ApplicationUser { UserName = "pmail@mail.com", CreatedOn = DateTime.Now.AddMonths(-2) },
                new ApplicationUser { UserName = "pmail1@mail.com", CreatedOn = DateTime.Now.AddMonths(-1) },
                new ApplicationUser { UserName = "pmail2@mail.com", CreatedOn = DateTime.Now },
                new ApplicationUser { UserName = "pmail3@mail.com", CreatedOn = DateTime.Now.AddMonths(-3) },
            };

            var mockRepoUser = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockRepoUser.Setup(x => x.All()).Returns(listUser.AsQueryable);

            var service = new StatisticsService(mockRepoUser.Object);
            var result = service.GetRegisteredUserCountForPast3Months();
            int expectedUsers = 0;
            foreach (var item in result.MonthData)
            {
                expectedUsers += item.RegisteredUsersCount;
            }

            Assert.Equal(3, expectedUsers);
        }

        [Fact]
        public void GetRegisteredUsersCountShouldWorkCorrectly()
        {
            var listUser = new List<ApplicationUser>()
            {
                new ApplicationUser { UserName = "pmail@mail.com", CreatedOn = DateTime.Now.AddMonths(-2) },
                new ApplicationUser { UserName = "pmail1@mail.com", CreatedOn = DateTime.Now.AddMonths(-1) },
                new ApplicationUser { UserName = "pmail2@mail.com", CreatedOn = DateTime.Now },
                new ApplicationUser { UserName = "pmail3@mail.com", CreatedOn = DateTime.Now.AddMonths(-3) },
            };

            var mockRepoUser = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockRepoUser.Setup(x => x.All()).Returns(listUser.AsQueryable);

            var service = new StatisticsService(mockRepoUser.Object);
            var userCount = service.GetRegisteredUsersCount();

            Assert.Equal(4, userCount);
        }
    }
}
