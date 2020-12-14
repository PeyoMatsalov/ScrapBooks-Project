namespace ScrapBookProject.Web.ViewModels.Pages
{
    using System;
    using System.Collections.Generic;

    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class ScrapBookPagesViewModel : ScrapBookViewModel
    {
        public int PageNumber { get; set; }

        public int PagesPerPage { get; set; }

        public int BookPagesCount => (int)Math.Ceiling((double)this.TotalBookPagesCount / this.PagesPerPage);

        public int TotalBookPagesCount { get; set; }

        public bool HasAnyPages => this.TotalBookPagesCount < 1;

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.BookPagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public ICollection<PageViewModel> Pages { get; set; }
    }
}
