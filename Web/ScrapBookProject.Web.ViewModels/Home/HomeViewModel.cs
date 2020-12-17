namespace ScrapBookProject.Web.ViewModels.Home
{
    using Ganss.XSS;

    public class HomeViewModel
    {
        public string Categories { get; set; }

        public string SanitizedCategories => new HtmlSanitizer().Sanitize(this.Categories);

        public string CategorieValues { get; set; }

        public string SanitizedCategorieValues => new HtmlSanitizer().Sanitize(this.CategorieValues);
    }
}
