namespace ScrapBookProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Services.Data;
    using ScrapBookProject.Web.ViewModels.Browse;

    [Authorize]
    public class BrowseController : BaseController
    {
        private readonly IBrowseService browseService;

        public BrowseController(IBrowseService browseService)
        {
            this.browseService = browseService;
        }

        public IActionResult Categories()
        {
            return this.View(this.browseService.GetAllCategories());
        }

        public IActionResult Category(int id)
        {
            var category = this.browseService.GetScrapBooksByCategoryId(id);
            return this.View(category);
        }
    }
}
