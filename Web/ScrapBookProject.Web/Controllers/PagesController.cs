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

        public PagesController(IPagesService pagesService)
        {
            this.pagesService = pagesService;
        }

        public IActionResult Pages(int id)
        {
            var pages = this.pagesService.GetPagesByBookId(id);

            return this.View(pages);
        }

        public IActionResult AddPage(int id)
        {
            return this.View(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddPage(int id, CreatePageInputModel input)
        {
            await this.pagesService.CreateAsync(input, id);
            return this.Redirect("/Pages/Pages");
        }
    }
}
