namespace ScrapBookProject.Data.Models
{
    using ScrapBookProject.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public int ScrapBookId { get; set; }

        public virtual ScrapBook ScrapBook { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}
