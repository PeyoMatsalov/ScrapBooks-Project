namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ScrapBookProject.Web.ViewModels.Administration;

    public interface IAdministrationSevice
    {
        Task CreateCategoryAsync(CreateCategoryInputModel input);

        ICollection<ManageCategoriesViewModel> GetAllCategories();

        Task DeleteCategory(int categoryId);
    }
}
