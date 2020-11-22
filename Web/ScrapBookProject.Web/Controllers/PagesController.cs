namespace ScrapBookProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.ViewModels.Pages;

    [Authorize]
    public class PagesController : BaseController
    {
        private readonly IPagesService pagesService;
        private readonly IScrapBooksService scrapBooksService;

        public PagesController(IPagesService pagesService, IScrapBooksService scrapBooksService)
        {
            this.pagesService = pagesService;
            this.scrapBooksService = scrapBooksService;
        }

        public IActionResult Pages(int id)
        {
            //var pages = this.pagesService.GetPagesByBookId(id);
            var scrapBook = this.scrapBooksService.GetScrapBookWithPagesById(id);
            var viewModel = new ScrapBookPagesViewModel()
            {
                Name = scrapBook.Name,
                Id = scrapBook.Id,
                Pages = scrapBook.Pages,
            };

            return this.View(viewModel);
        }

        public IActionResult AddPage()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPage(CreatePageInputModel input)
        {
            await this.pagesService.CreateAsync(input);
            return this.Redirect("/Pages/Pages");
        }
    }
}
