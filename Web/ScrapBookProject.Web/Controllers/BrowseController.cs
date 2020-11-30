namespace ScrapBookProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Services.Data;

    public class BrowseController : BaseController
    {
        private readonly IBrowseService browseService;

        public BrowseController(IBrowseService browseService)
        {
            this.browseService = browseService;
        }

        public IActionResult Browse()
        {
            var publicScrapBooks = this.browseService.GetAllPublicScrapBooks();
            return this.View(publicScrapBooks);
        }
    }
}
