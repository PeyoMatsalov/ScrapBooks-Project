using Ganss.XSS;

namespace ScrapBookProject.Web.ViewModels.Pages
{
    public class PageViewModel
    {
        public int BookId { get; set; }

        public int Number { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public bool HasPreviousPage => this.Number > 0;

        public int PreviousPageNumber => this.Number - 1;
    }
}
