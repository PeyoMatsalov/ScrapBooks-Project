namespace ScrapBookProject.Web.Controllers
{
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

        public IActionResult Pages(int id = 1)
        {
            int bookId = (int)this.TempData.Peek("bookId");
            var scrapBook = this.scrapBooksService.GetScrapBookWithPagesById(bookId);
            var viewModel = new ScrapBookPagesViewModel()
            {
                Name = scrapBook.Name,
                CreatorId = scrapBook.CreatorId,
                Id = scrapBook.Id,
                PageNumber = id,
                PagesPerPage = 2,
                TotalBookPagesCount = this.pagesService.GetPagesCountByBookId(bookId),
                Pages = this.pagesService.GetCurrentPages(bookId, id, 2).ToList(),
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
            int bookId = int.Parse(this.TempData["bookId"].ToString());

            await this.pagesService.CreateAsync(input, bookId);
            return this.RedirectToAction("Pages");
        }
    }
}
