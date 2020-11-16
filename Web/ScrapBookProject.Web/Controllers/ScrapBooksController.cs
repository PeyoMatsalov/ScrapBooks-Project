namespace ScrapBookProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;
    using System.Threading.Tasks;

    public class ScrapBooksController : Controller
    {
        private readonly IScrapBooksService scrapBooksService;

        public ScrapBooksController(IScrapBooksService scrapBooksService)
        {
            this.scrapBooksService = scrapBooksService;
        }

        public IActionResult ScrapBooks()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateScrapBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.scrapBooksService.CreateAsync(input);

            return this.Redirect("/");
        }
    }
}
