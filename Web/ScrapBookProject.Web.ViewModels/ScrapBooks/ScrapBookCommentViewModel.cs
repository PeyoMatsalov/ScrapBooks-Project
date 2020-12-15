namespace ScrapBookProject.Web.ViewModels.ScrapBooks
{
    using System;

    using Ganss.XSS;

    public class ScrapBookCommentViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string OwnerName { get; set; }
    }
}
