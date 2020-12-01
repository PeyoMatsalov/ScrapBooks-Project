namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ScrapBookProject.Web.ViewModels.Browse;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public interface IBrowseService
    {
        ICollection<ScrapBookViewModel> GetAllPublicScrapBooks();

        ICollection<CategoryViewModel> GetAllCategories();

        ICollection<ScrapBookViewModel> GetScrapBooksByCategoryId(int id);
    }
}
