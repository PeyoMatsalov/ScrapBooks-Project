namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Administration;

    public class AdministrationService : IAdministrationSevice
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;

        public AdministrationService(IDeletableEntityRepository<Category> categoriesRepository, IDeletableEntityRepository<ScrapBook> scrapBooksRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.scrapBooksRepository = scrapBooksRepository;
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

        public async Task UpdateAsync(int id, EditCategoryInputModel input)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);
            category.Name = input.Name;
            category.ImageUrl = input.ImgUrl;

            await this.scrapBooksRepository.SaveChangesAsync();
        }
    }
}
