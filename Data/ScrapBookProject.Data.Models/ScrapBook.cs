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
            this.Tags = new HashSet<Tag>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PagesCount { get; set; }

        public bool IsPrivate { get; set; }

        public string CoverUlr { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Page> Pages{ get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}
