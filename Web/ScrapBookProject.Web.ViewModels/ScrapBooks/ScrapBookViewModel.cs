namespace ScrapBookProject.Web.ViewModels.ScrapBooks
{
    public class ScrapBookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
}
