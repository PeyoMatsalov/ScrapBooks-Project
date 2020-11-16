namespace ScrapBookProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ScrapBooksController : Controller
    {
        public IActionResult ScrapBooks()
        {
            return this.View();
        }
    }
}
