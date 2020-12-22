namespace ScrapBookProject.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
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
        private readonly IStatisticsService statisticsService;

        public AdministrationController(
            IAdministrationSevice administrationSevice,
            ICategoriesService categoriesService,
            IStatisticsService statisticsService)
        {
            this.administrationSevice = administrationSevice;
            this.categoriesService = categoriesService;
            this.statisticsService = statisticsService;
        }

        public IActionResult AdminPanel()
        {
            var viewModel = this.statisticsService.GetRegisteredUserCountForPast3Months();
            return this.View(viewModel);
        }

        public IActionResult ManageCategories()
        {
            var categoriesViewModel = this.categoriesService.GetAllCategories();
            return this.View(categoriesViewModel);
        }

        public IActionResult ManageUsers()
        {
            return this.View(this.administrationSevice.GetAllUsers());
        }

        [HttpGet]
        public JsonResult GetSearchData(string searchBy, string searchValue)
        {
            var result = this.administrationSevice.GetUserInfo(searchBy, searchValue);
            return this.Json(result);
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

            await this.categoriesService.CreateCategoryAsync(input);

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

            this.categoriesService.UpdateCategoryAsync(input);
            return this.RedirectToAction(nameof(this.ManageCategories));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await this.categoriesService.DeleteCategoryAsync(id);

            return this.RedirectToAction(nameof(this.ManageCategories));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await this.administrationSevice.DeleteUserAsync(id);

            return this.RedirectToAction(nameof(this.ManageUsers));
        }
    }
}
