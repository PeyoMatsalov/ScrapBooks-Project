namespace ScrapBookProject.Web.ViewModels.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class ScrapBookPagesViewModel : ScrapBookViewModel
    {
        public ICollection<Page> Pages { get; set; }
    }
}
