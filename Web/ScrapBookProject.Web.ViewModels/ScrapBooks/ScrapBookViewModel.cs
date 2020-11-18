﻿namespace ScrapBookProject.Web.ViewModels.ScrapBooks
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ScrapBookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CoverUrl { get; set; }
    }
}
