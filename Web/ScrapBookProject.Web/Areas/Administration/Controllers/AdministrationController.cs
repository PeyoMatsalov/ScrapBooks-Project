namespace ScrapBookProject.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Common;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.Controllers;
    using ScrapBookProject.Web.ViewModels.Administration;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IAdministrationSevice administrationSevice;
        private readonly ICategoriesService categoriesService;

        public AdministrationController(IAdministrationSevice administrationSevice, ICategoriesService categoriesService)
        {
            this.administrationSevice = administrationSevice;
            this.categoriesService = categoriesService;
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

        public IActionResult EditCategory(int id)
        {
            var dbModel = this.categoriesService.GetCategoryById(id);
            var viewModel = new EditCategoryInputModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                ImgUrl = dbModel.ImageUrl,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCategory(EditCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.administrationSevice.UpdateCategoryAsync(input);
            return this.RedirectToAction(nameof(this.ManageCategories));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await this.administrationSevice.DeleteCategoryAsync(id);

            return this.RedirectToAction(nameof(this.ManageCategories));
        }
    }
}
