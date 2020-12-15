namespace ScrapBookProject.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Services.Data;

    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Categories()
        {
            return this.View(this.categoriesService.GetAllCategories());
        }

        public IActionResult Category(int id)
        {
            var category = this.categoriesService.GetScrapBooksByCategoryId(id);
            return this.View(category);
        }
    }
}
