namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ScrapBookProject.Web.ViewModels.Administration;

    public interface IAdministrationSevice
    {
        Task CreateCategoryAsync(CreateCategoryInputModel input);

        ICollection<ManageCategoriesViewModel> GetAllCategories();

        Task UpdateCategoryAsync(EditCategoryInputModel input);

        Task DeleteCategoryAsync(int categoryId);

        ICollection<UserViewModel> GetUserInfo(string searchBy, string searchValue);

        ICollection<UserViewModel> GetAllUsers();

        Task DeleteUserAsync(string userId);
    }
}
