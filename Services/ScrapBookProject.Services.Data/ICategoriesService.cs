namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ScrapBookProject.Web.ViewModels.Categories;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public interface ICategoriesService
    {
        ICollection<ScrapBookViewModel> GetAllPublicScrapBooks();

        ICollection<CategoryViewModel> GetAllCategories();

        ICollection<ScrapBookViewModel> GetScrapBooksByCategoryId(int id);
    }
}
