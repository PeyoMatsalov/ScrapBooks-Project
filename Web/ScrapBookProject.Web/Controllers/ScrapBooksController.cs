namespace ScrapBookProject.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class ScrapBooksController : Controller
    {
        private readonly IScrapBooksService scrapBooksService;

        public ScrapBooksController(IScrapBooksService scrapBooksService)
        {
            this.scrapBooksService = scrapBooksService;
        }

        public IActionResult ScrapBooks()
        {
            var scrapBooks = this.scrapBooksService.GetAllScrapBooks();
            return this.View(scrapBooks);
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

            return this.Redirect("/ScrapBooks/ScrapBooks");
        }
    }
}
