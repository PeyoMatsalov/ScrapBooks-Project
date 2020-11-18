namespace ScrapBookProject.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    [Authorize]
    public class ScrapBooksController : BaseController
    {
        private readonly IScrapBooksService scrapBooksService;
        private readonly UserManager<ApplicationUser> userManager;

        public ScrapBooksController(IScrapBooksService scrapBooksService, UserManager<ApplicationUser> userManager)
        {
            this.scrapBooksService = scrapBooksService;
            this.userManager = userManager;
        }

        public IActionResult ScrapBooks()
        {
            var userId = this.userManager.GetUserId(this.User);

            var scrapBooks = this.scrapBooksService.GetAllScrapBooksForUser(userId);
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

            var userId = this.userManager.GetUserId(this.User);

            await this.scrapBooksService.CreateAsync(input, userId);

            return this.Redirect("/ScrapBooks/ScrapBooks");
        }

        public IActionResult CurrentScrapBook()
        {
            var viewModel = this.scrapBooksService.GetScrapBookById();
            return this.View(viewModel);
        }
    }
}
