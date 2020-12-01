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
            this.Pages = new HashSet<Page>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Visibility { get; set; }

        public string CoverUlr { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Page> Pages { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
