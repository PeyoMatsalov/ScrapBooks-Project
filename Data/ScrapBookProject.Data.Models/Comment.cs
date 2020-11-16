namespace ScrapBookProject.Data.Models
{
    using ScrapBookProject.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int ScrapBookId { get; set; }

        public virtual ScrapBook ScrapBook { get; set; }

        public int OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}
