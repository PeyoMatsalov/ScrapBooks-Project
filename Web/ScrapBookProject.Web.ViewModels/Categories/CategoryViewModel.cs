namespace ScrapBookProject.Web.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int ScrapBooksCount { get; set; }
    }
}
