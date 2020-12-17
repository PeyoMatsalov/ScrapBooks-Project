namespace ScrapBookProject.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;
    using Xunit;

    public class ScrapBooksServiceTests
    {
        [Fact]
        public async Task CreateAsyncShouldCreateAScrapbookSuccessfuly()
        {
            var listCat = new List<Category>();
            var mockRepoCat = new Mock<IDeletableEntityRepository<Category>>();
            mockRepoCat.Setup(x => x.All()).Returns(listCat.AsQueryable);

            var listPgs = new List<Page>();
            var mockRepoPgs = new Mock<IDeletableEntityRepository<Page>>();
            mockRepoPgs.Setup(x => x.All()).Returns(listPgs.AsQueryable);

            var listCmnts = new List<Comment>();
            var mockRepoCmnts = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepoCmnts.Setup(x => x.All()).Returns(listCmnts.AsQueryable);

            var listSb = new List<ScrapBook>();
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var listUser = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1" },
            };
            var mockRepoUser = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockRepoUser.Setup(x => x.All()).Returns(listUser.AsQueryable);

            var service = new ScrapBooksService(
                mockRepoSb.Object,
                mockRepoUser.Object,
                mockRepoPgs.Object,
                mockRepoCat.Object,
                mockRepoCmnts.Object);

            await service.CreateAsync(
                new CreateScrapBookInputModel
                {
                    Name = "Test",
                    Description = "Test",
                    CategoryId = 1,
                    CoverUrl = "Test",
                    Visibility = "Public",
                }, "1");

            Assert.Single(listSb);
        }

        [Fact]
        public async Task DeleteScrapBookAsyncShouldWorkCorrectly()
        {
            var listCat = new List<Category>();
            var mockRepoCat = new Mock<IDeletableEntityRepository<Category>>();
            mockRepoCat.Setup(x => x.All()).Returns(listCat.AsQueryable);

            var listPgs = new List<Page>();
            var mockRepoPgs = new Mock<IDeletableEntityRepository<Page>>();
            mockRepoPgs.Setup(x => x.All()).Returns(listPgs.AsQueryable);

            var listCmnts = new List<Comment>();
            var mockRepoCmnts = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepoCmnts.Setup(x => x.All()).Returns(listCmnts.AsQueryable);

            var listSb = new List<ScrapBook>()
            {
                new ScrapBook { Id = 1, CategoryId = 1, Name = "Test", Description = "Test" },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);

            var listUser = new List<ApplicationUser>();
            var mockRepoUser = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockRepoUser.Setup(x => x.All()).Returns(listUser.AsQueryable);

            var service = new ScrapBooksService(
                mockRepoSb.Object,
                mockRepoUser.Object,
                mockRepoPgs.Object,
                mockRepoCat.Object,
                mockRepoCmnts.Object);

            await service.DeleteScrapBookAsync(1);

            Assert.True(listSb.FirstOrDefault().IsDeleted);
        }

        [Fact]
        public void GetAllScrapBooksForUserShouldWorkCorrectly()
        {
            var listCat = new List<Category>();
            var mockRepoCat = new Mock<IDeletableEntityRepository<Category>>();
            mockRepoCat.Setup(x => x.All()).Returns(listCat.AsQueryable);

            var listPgs = new List<Page>();
            var mockRepoPgs = new Mock<IDeletableEntityRepository<Page>>();
            mockRepoPgs.Setup(x => x.All()).Returns(listPgs.AsQueryable);

            var listCmnts = new List<Comment>();
            var mockRepoCmnts = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepoCmnts.Setup(x => x.All()).Returns(listCmnts.AsQueryable);

            var listSb = new List<ScrapBook>()
            {
                new ScrapBook { Id = 1, CategoryId = 1, Name = "Test", Description = "Test", CreatorId = "1" },
                new ScrapBook { Id = 2, CategoryId = 2, Name = "Test", Description = "Test", CreatorId = "2" },
                new ScrapBook { Id = 3, CategoryId = 1, Name = "Test", Description = "Test", CreatorId = "1" },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var listUser = new List<ApplicationUser>()
            {
                new ApplicationUser { Id = "1" },
            };
            var mockRepoUser = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockRepoUser.Setup(x => x.All()).Returns(listUser.AsQueryable);

            var service = new ScrapBooksService(
                mockRepoSb.Object,
                mockRepoUser.Object,
                mockRepoPgs.Object,
                mockRepoCat.Object,
                mockRepoCmnts.Object);

            var resultList = service.GetAllScrapBooksForUser("1");

            Assert.Equal(2, resultList.Count());
        }

        [Fact]
        public void GetScrapBookByIdShouldReturnCorrectResult()
        {
            var listCat = new List<Category>()
            {
                new Category { Id = 1 },
            };
            var mockRepoCat = new Mock<IDeletableEntityRepository<Category>>();
            mockRepoCat.Setup(x => x.All()).Returns(listCat.AsQueryable);

            var listPgs = new List<Page>();
            var mockRepoPgs = new Mock<IDeletableEntityRepository<Page>>();
            mockRepoPgs.Setup(x => x.All()).Returns(listPgs.AsQueryable);

            var listCmnts = new List<Comment>();
            var mockRepoCmnts = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepoCmnts.Setup(x => x.All()).Returns(listCmnts.AsQueryable);

            var listSb = new List<ScrapBook>()
            {
                new ScrapBook { Id = 1, CategoryId = 1, Name = "Test", Description = "Test", CreatorId = "1" },
                new ScrapBook { Id = 2, CategoryId = 2, Name = "Test", Description = "Test", CreatorId = "2" },
                new ScrapBook { Id = 3, CategoryId = 1, Name = "Test", Description = "Test", CreatorId = "1" },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);

            var listUser = new List<ApplicationUser>()
            {
                new ApplicationUser { Id = "1" },
            };
            var mockRepoUser = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockRepoUser.Setup(x => x.All()).Returns(listUser.AsQueryable);

            var service = new ScrapBooksService(
                mockRepoSb.Object,
                mockRepoUser.Object,
                mockRepoPgs.Object,
                mockRepoCat.Object,
                mockRepoCmnts.Object);

            var result = service.GetScrapBookById(1);
            var expectedResult = listSb.FirstOrDefault(x => x.Id == 1);

            Assert.Equal(expectedResult.Name, result.Name);
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Description, result.Description);
            Assert.Equal(expectedResult.CreatorId, result.CreatorId);
        }

        [Fact]
        public async Task UpdateAsyncShouldWorkProperly()
        {
            var listCat = new List<Category>();
            var mockRepoCat = new Mock<IDeletableEntityRepository<Category>>();
            mockRepoCat.Setup(x => x.All()).Returns(listCat.AsQueryable);

            var listPgs = new List<Page>();
            var mockRepoPgs = new Mock<IDeletableEntityRepository<Page>>();
            mockRepoPgs.Setup(x => x.All()).Returns(listPgs.AsQueryable);

            var listCmnts = new List<Comment>();
            var mockRepoCmnts = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepoCmnts.Setup(x => x.All()).Returns(listCmnts.AsQueryable);

            var listSb = new List<ScrapBook>()
            {
                new ScrapBook { Id = 1, CategoryId = 1, Name = "Test", Description = "Test", CoverUlr = "Test" },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var listUser = new List<ApplicationUser>()
            {
                new ApplicationUser { Id = "1" },
            };
            var mockRepoUser = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockRepoUser.Setup(x => x.All()).Returns(listUser.AsQueryable);

            var service = new ScrapBooksService(
                mockRepoSb.Object,
                mockRepoUser.Object,
                mockRepoPgs.Object,
                mockRepoCat.Object,
                mockRepoCmnts.Object);

            await service.UpdateAsync(1, new EditScrapBookInputModel
            {
                Name = "CorrectName",
                Description = "CorrectDescription",
                CoverUrl = "CorrectCoverUrl",
                CategoryId = 1,
                Visibility = "Private",
            });

            Assert.Equal("CorrectName", listSb.FirstOrDefault().Name);
            Assert.Equal("CorrectDescription", listSb.FirstOrDefault().Description);
            Assert.Equal("CorrectCoverUrl", listSb.FirstOrDefault().CoverUlr);
            Assert.Equal(1, listSb.FirstOrDefault().CategoryId);
            Assert.Equal("Private", listSb.FirstOrDefault().Visibility);
        }
    }
}
