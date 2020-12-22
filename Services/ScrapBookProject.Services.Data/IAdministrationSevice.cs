namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ScrapBookProject.Web.ViewModels.Administration;

    public interface IAdministrationSevice
    {
        ICollection<UserViewModel> GetUserInfo(string searchBy, string searchValue);

        ICollection<UserViewModel> GetAllUsers();

        Task DeleteUserAsync(string userId);
    }
}
