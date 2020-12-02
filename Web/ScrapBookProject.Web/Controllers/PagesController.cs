namespace ScrapBookProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.ViewModels.Pages;

    [Authorize]
    public class PagesController : BaseController
    {
        private readonly IPagesService pagesService;
        private readonly IScrapBooksService scrapBooksService;
        private readonly UserManager<ApplicationUser> userManager;

        public PagesController(IPagesService pagesService, IScrapBooksService scrapBooksService, UserManager<ApplicationUser> userManager)
        {
            this.pagesService = pagesService;
            this.scrapBooksService = scrapBooksService;
            this.userManager = userManager;
        }

        public IActionResult Pages(int id)
        {
            this.Response.Cookies.Append("BookId", id.ToString(), new CookieOptions() { Expires = DateTimeOffset.Now.AddHours(1), SameSite = SameSiteMode.Strict });
            var scrapBook = this.scrapBooksService.GetScrapBookWithPagesById(id);
            var viewModel = new ScrapBookPagesViewModel()
            {
                Name = scrapBook.Name,
                CreatorId = scrapBook.CreatorId,
                Id = scrapBook.Id,
                Pages = scrapBook.Pages,
            };
            return this.View(viewModel);
        }

        public IActionResult AddPage()
        {
            this.ViewData["BookId"] = this.Request.Cookies["BookId"];

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPage(CreatePageInputModel input)
        {
            int bookId = int.Parse(this.Request.Cookies["BookId"]);

            await this.pagesService.CreateAsync(input, bookId);
            return this.Redirect($"/Pages/Pages/{bookId}");
        }
    }
}
