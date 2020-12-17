namespace ScrapBookProject.Web.ViewModels.Home
{
    using Ganss.XSS;

    public class HomeViewModel
    {
        public int UsersCount { get; set; }

        public int CategoriesCount { get; set; }

        public int ScrapBooksCount { get; set; }

        public int PagesCount { get; set; }

        public string Categories { get; set; }

        public string SanitizedCategories => new HtmlSanitizer().Sanitize(this.Categories);

        public string CategorieValues { get; set; }

        public string SanitizedCategorieValues => new HtmlSanitizer().Sanitize(this.CategorieValues);
    }
}
