namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Administration;
    using ScrapBookProject.Web.ViewModels.Categories;
    using ScrapBookProject.Web.ViewModels.Home;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IStringService stringService;

        public CategoriesService(
            IDeletableEntityRepository<ScrapBook> scrapBooksRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IStringService stringService)
        {
            this.scrapBooksRepository = scrapBooksRepository;
            this.categoriesRepository = categoriesRepository;
            this.stringService = stringService;
        }

        public async Task UpdateCategoryAsync(EditCategoryInputModel input)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == input.Id);
            category.Name = input.Name;
            category.ImageUrl = input.ImgUrl;

            await this.categoriesRepository.SaveChangesAsync();
        }

        public ICollection<ManageCategoriesViewModel> GetAllCategories()
        {
            return this.categoriesRepository.All()
                .OrderBy(x => x.Name)
                .Select(x => new ManageCategoriesViewModel
                {
                    CategoryId = x.Id,
                    Name = x.Name,
                    BooksCount = this.scrapBooksRepository.All().Where(y => y.CategoryId == x.Id).Count(),
                    DateCreated = x.CreatedOn.ToString("dd-MM-y"),
                }).ToList();
        }

        public async Task CreateCategoryAsync(CreateCategoryInputModel input)
        {
            var category = new Category
            {
                Name = input.Name,
                ImageUrl = input.ImgUrl,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == categoryId);
            category.IsDeleted = true;
            await this.categoriesRepository.SaveChangesAsync();
        }

        public ICollection<CategoryViewModel> GetAllCategoryNamesAndBooksCount()
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

        public HomeCategoryInfoViewModel GetCategorySbsCountsOrdered()
        {
            var categoryInfos = new List<CategoryInfo>();
            foreach (var category in this.categoriesRepository.All())
            {
                CategoryInfo currCategory = new CategoryInfo
                {
                    Name = category.Name,
                    ScrapBooksCount = this.scrapBooksRepository.All().Where(x => x.CategoryId == category.Id).Count(),
                };
                categoryInfos.Add(currCategory);
            }

            categoryInfos.OrderBy(x => x.Name);

            var categoryNames = new List<string>();
            var categoryData = new List<int>();

            foreach (var category in categoryInfos)
            {
                categoryNames.Add(category.Name);
                categoryData.Add(category.ScrapBooksCount);
            }

            return new HomeCategoryInfoViewModel
            {
                Categories = this.stringService.ConvertCollectionOfStringToStringForChartJS(categoryNames),
                CategorieValues = this.stringService.ConvertCollectionOfIntToStringForChartJS(categoryData),
            };
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
