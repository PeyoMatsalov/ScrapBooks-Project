namespace ScrapBookProject.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Pages;
    using Xunit;

    public class PagesServiceTests
    {
        [Fact]
        public async Task CreateAsyncShouldCreateAPageCorrectly()
        {
            var list = new List<Page>();
            var mockRepo = new Mock<IDeletableEntityRepository<Page>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Page>())).Callback((Page page) => list.Add(page));
            var service = new PagesService(mockRepo.Object);

            var page = new CreatePageInputModel { ScrapBookId = 1, Content = "Test", PageNumber = 1 };
            await service.CreateAsync(page, page.ScrapBookId);

            Assert.Single(list);
        }

        [Fact]
        public void GetPagesByBookIdShouldReturnThePagesForTheCorrectBook()
        {
            var list = new List<Page>
            {
                new Page { ScrapBookId = 1, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 2, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 6, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 2, Content = "Test" },
                new Page { ScrapBookId = 5, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 3, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 4, Content = "Test" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Page>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            var service = new PagesService(mockRepo.Object);

            var resultList = service.GetPagesByBookId(1);

            Assert.Equal(4, resultList.Count());
        }

        [Fact]
        public void GetPagesCountByBookIdShouldReturnTheCorrectResult()
        {
            var list = new List<Page>
            {
                new Page { ScrapBookId = 1, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 2, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 6, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 2, Content = "Test" },
                new Page { ScrapBookId = 5, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 3, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 4, Content = "Test" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Page>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            var service = new PagesService(mockRepo.Object);

            var result = service.GetPagesCountByBookId(1);

            Assert.Equal(4, result);
        }

        [Fact]
        public void GetCurrentPagesShouldReturnTheCurrentPages()
        {
            var list = new List<Page>
            {
                new Page { ScrapBookId = 1, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 2, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 6, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 2, Content = "Test" },
                new Page { ScrapBookId = 5, PageNumber = 1, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 3, Content = "Test" },
                new Page { ScrapBookId = 1, PageNumber = 4, Content = "Test" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Page>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            var service = new PagesService(mockRepo.Object);

            List<PageViewModel> resultList = service.GetCurrentPages(1, 1, 2).ToList();

            Assert.Equal(1, resultList.First().PageNumber);
            Assert.Equal(1, resultList.Skip(1).FirstOrDefault().PageNumber);
        }
    }
}
