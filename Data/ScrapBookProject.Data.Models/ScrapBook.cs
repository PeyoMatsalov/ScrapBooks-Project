namespace ScrapBookProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ScrapBookProject.Data.Common.Models;

    public class ScrapBook : BaseDeletableModel<int>
    {
        public ScrapBook()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PagesCount { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}
