namespace ScrapBookProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;
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
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var listSb = new List<ScrapBook>();
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object);

            var resultList = service.GetAllCategories();

            Assert.Equal(3, resultList.Count);
        }

        [Fact]
        public void GetAllPublicScrapBooksShouldReturnAllPublicScrapBooks()
        {
            var list = new List<Category>();
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var listSb = new List<ScrapBook>
            {
                new ScrapBook { Id = 1, Name = "Test", Visibility = "Public" },
                new ScrapBook { Id = 2, Name = "Test", Visibility = "Private" },
                new ScrapBook { Id = 3, Name = "Test", Visibility = "Private" },
                new ScrapBook { Id = 4, Name = "Test", Visibility = "Public" },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object);

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

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object);

            var result = service.GetCategoryById(1);

            Assert.Equal(list.FirstOrDefault(), result);
        }

        [Fact]
        public void GetScrapBooksByCategoryIdShouldReturnTheCorrectScrapBooks()
        {
            var list = new List<Category>();
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var listSb = new List<ScrapBook>
            {
                new ScrapBook { Id = 1, Name = "Test", Visibility = "Public", CategoryId = 1 },
                new ScrapBook { Id = 2, Name = "Test", Visibility = "Private", CategoryId = 3 },
                new ScrapBook { Id = 3, Name = "Test", Visibility = "Private", CategoryId = 6 },
                new ScrapBook { Id = 4, Name = "Test", Visibility = "Public", CategoryId = 1 },
                new ScrapBook { Id = 3, Name = "Test", Visibility = "Private", CategoryId = 1 },
            };
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var service = new CategoriesService(mockRepoSb.Object, mockRepo.Object);

            List<ScrapBookViewModel> resultList = service.GetScrapBooksByCategoryId(1).ToList();

            Assert.Equal(3, resultList.Count());
        }
    }
}
