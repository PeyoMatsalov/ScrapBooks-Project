namespace ScrapBookProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ScrapBookPagesController : Controller
    {
        public IActionResult CurrentScrapBook()
        {
            return this.View();
        }
    }
}
