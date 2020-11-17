namespace ScrapBookProject.Data.Models
{
    using ScrapBookProject.Data.Common.Models;

    public class Page : BaseDeletableModel<int>
    {
        public int PageNumber { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public int ScrapBookId { get; set; }

        public virtual ScrapBook ScrapBook { get; set; }
    }
}
