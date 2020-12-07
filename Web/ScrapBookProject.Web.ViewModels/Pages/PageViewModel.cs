namespace ScrapBookProject.Web.ViewModels.Pages
{
    using System;

    using Ganss.XSS;

    public class PageViewModel
    {
        public string BookName { get; set; }

        public int BookId { get; set; }

        public int PageNumber { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        // Number of pages within the book
        public int TotalBookPagesCount { get; set; }

        // The number of pages used for pagination
        public int PagesCount => (int)Math.Ceiling((double)this.TotalBookPagesCount / this.PagesPerPage);

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesPerPage { get; set; }
    }
}
