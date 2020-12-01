﻿namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Browse;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class BrowseService : IBrowseService
    {
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public BrowseService(IDeletableEntityRepository<ScrapBook> scrapBooksRepository, IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.scrapBooksRepository = scrapBooksRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public ICollection<CategoryViewModel> GetAllCategories()
        {
            return this.categoriesRepository.All()
                .OrderBy(x => x.Name)
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public ICollection<ScrapBookViewModel> GetAllPublicScrapBooks()
        {
            var scrapBooksDb = this.scrapBooksRepository.All().Where(x => x.Visibility == "Public" && x.IsDeleted == false).ToList();
            var resultScrapBooks = new List<ScrapBookViewModel>();

            foreach (var scrapBookDbModel in scrapBooksDb)
            {
                var scrapBook = new ScrapBookViewModel
                {
                    Id = scrapBookDbModel.Id,
                    Name = scrapBookDbModel.Name,
                    Description = scrapBookDbModel.Description,
                    CoverUrl = scrapBookDbModel.CoverUlr,
                    IsDeleted = scrapBookDbModel.IsDeleted,
                };

                resultScrapBooks.Add(scrapBook);
            }

            return resultScrapBooks;
        }

        public ICollection<ScrapBookViewModel> GetScrapBooksByCategoryId(int categoryId)
        {
            var scrapBooks = this.scrapBooksRepository.All()
                .Where(x => x.Category.Id == categoryId && x.Visibility == "Public" && x.IsDeleted == false)
                .Select(x => new ScrapBookViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CoverUrl = x.CoverUlr,
                })
                .ToList();

            return scrapBooks;
        }
    }
}
