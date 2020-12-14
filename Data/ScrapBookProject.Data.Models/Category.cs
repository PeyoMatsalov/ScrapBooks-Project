namespace ScrapBookProject.Data.Models
{
    using ScrapBookProject.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
