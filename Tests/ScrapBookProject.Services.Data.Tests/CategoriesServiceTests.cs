namespace ScrapBookProject.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetAllCategoriesShouldReturnAllCategories()
        {
            var list = new List<Category>()
            {
                new Category { Id = 1, Name = "Test", ImageUrl = "Test" },
                new Category { Id = 2, Name = "Test", ImageUrl = "Test" },
                new Category { Id = 3, Name = "Test", ImageUrl = "Test" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);

            var listSb = new List<ScrapBook>();
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object, new StringService());

            var resultList = service.GetAllCategories();

            Assert.Equal(3, resultList.Count);
        }

        [Fact]
        public void GetAllPublicScrapBooksShouldReturnAllPublicScrapBooks()
        {
            var list = new List<Category>();
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);

            var listSb = new List<ScrapBook>
            {
                new ScrapBook { Id = 1, Name = "Test", Visibility = "Public" },
                new ScrapBook { Id = 2, Name = "Test", Visibility = "Private" },
                new ScrapBook { Id = 3, Name = "Test", Visibility = "Private" },
                new ScrapBook { Id = 4, Name = "Test", Visibility = "Public" },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object, new StringService());

            var resultList = service.GetAllPublicScrapBooks();

            Assert.Equal(2, resultList.Count);
        }

        [Fact]
        public void GetCategoryByIdShouldReturnTheCorrectCategory()
        {
            var list = new List<Category>()
            {
                new Category { Id = 1, Name = "Correct", ImageUrl = "Correct" },
                new Category { Id = 2, Name = "Wrong", ImageUrl = "Wrong" },
                new Category { Id = 3, Name = "Test", ImageUrl = "Test" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var listSb = new List<ScrapBook>();
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object, new StringService());

            var result = service.GetCategoryById(1);

            Assert.Equal(list.FirstOrDefault(), result);
        }

        [Fact]
        public void GetScrapBooksByCategoryIdShouldReturnTheCorrectScrapBooks()
        {
            var list = new List<Category>()
            {
                new Category { Id = 1 },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);

            var listSb = new List<ScrapBook>
            {
                new ScrapBook { Id = 1, Name = "Test", Visibility = "Public", CategoryId = 1, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 2, Name = "Test", Visibility = "Private", CategoryId = 3, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 3, Name = "Test", Visibility = "Private", CategoryId = 6, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 4, Name = "Test", Visibility = "Public", CategoryId = 1, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 5, Name = "Test", Visibility = "Private", CategoryId = 1, Description = "Test", IsDeleted = false },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object, new StringService());

            var resultList = service.GetScrapBooksByCategoryId(1);

            Assert.Equal(3, resultList.Count());
        }

        [Fact]
        public void GetCategorySbsCountsOrderedShouldWorkCorrectly()
        {
            var list = new List<Category>()
            {
                new Category { Id = 1, Name = "Test", ImageUrl = "Test" },
                new Category { Id = 2, Name = "Test2", ImageUrl = "Test" },
                new Category { Id = 3, Name = "Test3", ImageUrl = "Test" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);

            var listSb = new List<ScrapBook>()
            {
                new ScrapBook { Id = 1, Name = "Test", Visibility = "Public", CategoryId = 1, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 2, Name = "Test2", Visibility = "Private", CategoryId = 2, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 3, Name = "Test3", Visibility = "Private", CategoryId = 2, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 4, Name = "Test4", Visibility = "Public", CategoryId = 1, Description = "Test", IsDeleted = false },
                new ScrapBook { Id = 5, Name = "Test5", Visibility = "Private", CategoryId = 1, Description = "Test", IsDeleted = false },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object, new StringService());

            var result = service.GetCategorySbsCountsOrdered();
            var expectedCategoryString = "[\"Test\",\"Test2\",\"Test3\"]";
            var expectedCategoryValueString = "[3,2,0]";

            Assert.Equal(expectedCategoryString, result.Categories);
            Assert.Equal(expectedCategoryValueString, result.CategorieValues);
        }
    }
}
