namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Administration;
    using ScrapBookProject.Web.ViewModels.Categories;
    using ScrapBookProject.Web.ViewModels.Home;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public interface ICategoriesService
    {
        Task CreateCategoryAsync(CreateCategoryInputModel input);

        ICollection<ManageCategoriesViewModel> GetAllCategories();

        Task UpdateCategoryAsync(EditCategoryInputModel input);

        Task DeleteCategoryAsync(int categoryId);

        ICollection<ScrapBookViewModel> GetAllPublicScrapBooks();

        ICollection<CategoryViewModel> GetAllCategoryNamesAndBooksCount();

        ICollection<ScrapBookViewModel> GetScrapBooksByCategoryId(int id);

        Category GetCategoryById(int id);

        HomeCategoryInfoViewModel GetCategorySbsCountsOrdered();
    }
}
