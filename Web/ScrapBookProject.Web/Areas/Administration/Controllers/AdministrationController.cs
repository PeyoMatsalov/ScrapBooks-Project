namespace ScrapBookProject.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Common;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.Controllers;
    using ScrapBookProject.Web.ViewModels.Administration;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IAdministrationSevice administrationSevice;

        public AdministrationController(IAdministrationSevice administrationSevice)
        {
            this.administrationSevice = administrationSevice;
        }

        public IActionResult AdminPanel()
        {
            return this.View();
        }

        public IActionResult ManageCategories()
        {
            var categoriesViewModel = this.administrationSevice.GetAllCategories();
            return this.View(categoriesViewModel);
        }

        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.administrationSevice.CreateCategoryAsync(input);

            return this.RedirectToAction(nameof(this.ManageCategories));
        }
    }
}
