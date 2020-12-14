namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Categories;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<ScrapBook> scrapBooksRepository, IDeletableEntityRepository<Category> categoriesRepository)
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
                    ImageUrl = x.ImageUrl,
                    ScrapBooksCount = this.scrapBooksRepository.All().Where(y => y.CategoryId == x.Id && y.Visibility == "Public").Count(),
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

        public Category GetCategoryById(int id)
        {
            return this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);
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
