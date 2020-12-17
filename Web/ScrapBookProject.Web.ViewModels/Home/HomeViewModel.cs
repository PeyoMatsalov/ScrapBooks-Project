namespace ScrapBookProject.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public int UsersCount { get; set; }

        public int CategoriesCount { get; set; }

        public int ScrapBooksCount { get; set; }

        public int PagesCount { get; set; }

        public HomeCategoryInfoViewModel CategoryInfo { get; set; }
    }
}
