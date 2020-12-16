namespace ScrapBookProject.Web.ViewModels.Pages
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePageInputModel
    {
        public int ScrapBookId { get; set; }

        public int PageNumber { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }
    }
}
