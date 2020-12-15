﻿namespace ScrapBookProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Administration;
    using Xunit;

    public class AdministrationServiceTests
    {
        [Fact]
        public async Task CreateCategoryAsyncShouldCreateACategory()
        {
            var list = new List<Category>();
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var listSb = new List<ScrapBook>();
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var service = new AdministrationService(mockRepo.Object, mockRepoSb.Object);
            var category = new CreateCategoryInputModel { Name = "Test", ImgUrl = "Test" };

            await service.CreateCategoryAsync(category);

            Assert.Single(list);
        }

        [Fact]
        public async Task DeleteCategoryAsyncShouldDeleteCategory()
        {
            var list = new List<Category>();
            var category = new Category { Id = 1, Name = "Test", ImageUrl = "Test" };
            list.Add(category);
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var listSb = new List<ScrapBook>();
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var service = new AdministrationService(mockRepo.Object, mockRepoSb.Object);

            await service.DeleteCategoryAsync(1);

            Assert.True(list.First().IsDeleted);
        }

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

            var service = new AdministrationService(mockRepo.Object, mockRepoSb.Object);

            var resultList = service.GetAllCategories();

            Assert.Equal(3, resultList.Count);
        }

        [Fact]
        public async Task UpdateCategoryAsyncShouldUpdateTheCategory()
        {
            var list = new List<Category>();
            var category = new Category { Id = 1, Name = "Test", ImageUrl = "Test" };
            list.Add(category);

            EditCategoryInputModel updatedCategory = new EditCategoryInputModel { Id = 1, Name = "EditNameTest", ImgUrl = "EditImgUrlTest" };

            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => list.Add(category));

            var listSb = new List<ScrapBook>();
            var mockRepoSb = new Mock<IDeletableEntityRepository<ScrapBook>>();
            mockRepoSb.Setup(x => x.All()).Returns(listSb.AsQueryable);
            mockRepoSb.Setup(x => x.AddAsync(It.IsAny<ScrapBook>())).Callback((ScrapBook scrapBook) => listSb.Add(scrapBook));

            var service = new AdministrationService(mockRepo.Object, mockRepoSb.Object);

            await service.UpdateCategoryAsync(updatedCategory);

            Assert.Equal("EditNameTest", list.First().Name);
            Assert.Equal("EditImgUrlTest", list.First().ImageUrl);
        }
    }
}
