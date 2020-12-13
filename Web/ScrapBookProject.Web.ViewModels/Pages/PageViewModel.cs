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
    }
}
