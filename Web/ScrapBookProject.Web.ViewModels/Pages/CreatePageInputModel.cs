using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ScrapBookProject.Web.ViewModels.Pages
{
    public class CreatePageInputModel
    {
        public int PageNumber { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

    }
}
