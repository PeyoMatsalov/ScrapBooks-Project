namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;

    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Categories;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public interface ICategoriesService
    {
        ICollection<ScrapBookViewModel> GetAllPublicScrapBooks();

        ICollection<CategoryViewModel> GetAllCategories();

        ICollection<ScrapBookViewModel> GetScrapBooksByCategoryId(int id);

        Category GetCategoryById(int id);
    }
}
