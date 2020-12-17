namespace ScrapBookProject.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.ViewModels;
    using ScrapBookProject.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IPagesService pageService;
        private readonly IAdministrationSevice administrationSevice;
        private readonly IStatisticsService statisticsService;
        private readonly IScrapBooksService scrapBooksService;
        private readonly ICategoriesService categoriesService;

        public HomeController(
            IPagesService pageService,
            IAdministrationSevice administrationSevice,
            IStatisticsService statisticsService,
            IScrapBooksService scrapBooksService,
            ICategoriesService categoriesService)
        {
            this.pageService = pageService;
            this.administrationSevice = administrationSevice;
            this.statisticsService = statisticsService;
            this.scrapBooksService = scrapBooksService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                UsersCount = this.statisticsService.GetRegisteredUsersCount(),
                CategoriesCount = this.administrationSevice.GetAllCategories().Count,
                ScrapBooksCount = this.scrapBooksService.GetAllScrapBooksCount(),
                PagesCount = this.pageService.GetAllPagesCount(),
                CategoryInfo = this.categoriesService.GetCategorySbsCountsOrdered(),
            };
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
