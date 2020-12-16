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
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateCommentAsyncShouldCreateAComment()
        {
            var list = new List<Comment>();
            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment comment) => list.Add(comment));
            var service = new CommentsService(mockRepo.Object);

            await service.CreateCommentAsync(1, "testguid", "test", 1);

            Assert.Single(list);
        }

        [Fact]
        public void IsInScrapBookIdShouldReturnCorrectResult()
        {
            var list = new List<Comment>
            {
                new Comment { Id = 1, ScrapBookId = 1, Content = "test", OwnerId = "1", ParentId = 2 },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            var service = new CommentsService(mockRepo.Object);

            var resultTrue = service.IsInScrapBookId(1, 1);
            var resultFalse = service.IsInScrapBookId(1, 2);

            Assert.True(resultTrue);
            Assert.False(resultFalse);
        }
    }
}
