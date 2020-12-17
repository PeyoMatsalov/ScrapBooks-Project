namespace ScrapBookProject.Web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Services.Messaging;
    using ScrapBookProject.Web.ViewModels.Categories;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    [Authorize]
    public class ScrapBooksController : BaseController
    {
        private readonly IScrapBooksService scrapBooksService;
        private readonly ICategoriesService categoriesService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public ScrapBooksController(
            UserManager<ApplicationUser> userManager,
            IScrapBooksService scrapBooksService,
            ICategoriesService categoriesService,
            IEmailSender emailSender)
        {
            this.scrapBooksService = scrapBooksService;
            this.categoriesService = categoriesService;
            this.emailSender = emailSender;
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
            var categories = this.categoriesService.GetAllCategories().Select(x => new CategoryDropdownViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            var viewModel = new CreateScrapBookInputModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
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

            return this.RedirectToAction(nameof(this.ScrapBooks));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.scrapBooksService.DeleteScrapBookAsync(id);

            return this.RedirectToAction(nameof(this.ScrapBooks));
        }

        public IActionResult Edit(int id)
        {
            var scrapBook = this.scrapBooksService.GetScrapBookById(id);
            var viewModel = new EditScrapBookInputModel
            {
                Id = id,
                Name = scrapBook.Name,
                Description = scrapBook.Description,
                CoverUrl = scrapBook.CoverUrl,
                Visibility = scrapBook.Visibility,
                CategoryId = scrapBook.CategoryId,
            };

            viewModel.Categories = this.categoriesService.GetAllCategories().Select(x => new CategoryDropdownViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditScrapBookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAllCategories().Select(x => new CategoryDropdownViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                });
                return this.View(input);
            }

            await this.scrapBooksService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ScrapBooks));
        }

        public IActionResult Details(int id)
        {
            this.TempData["bookId"] = id;
            var viewModel = this.scrapBooksService.GetScrapBookById(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var scrapBook = this.scrapBooksService.GetScrapBookById(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{scrapBook.Name}</h1>");
            html.AppendLine($"<h3>{scrapBook.CategoryName}</h3>");
            html.AppendLine($"<h5>{scrapBook.Description}</h5>");

            await this.emailSender.SendEmailAsync("BookSevice@books.com", "ScrapBooks", "peiomatsalov@gmail.com", scrapBook.Name, html.ToString());

            return this.RedirectToAction(nameof(this.ScrapBooks));
        }
    }
}
