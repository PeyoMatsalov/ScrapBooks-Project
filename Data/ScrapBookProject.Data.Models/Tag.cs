namespace ScrapBookProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ScrapBookProject.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int ScrapBookId { get; set; }

        public virtual ScrapBook ScrapBook { get; set; }
    }
}
