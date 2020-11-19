namespace ScrapBookProject.Web.ViewModels.ScrapBooks
{
    using System.ComponentModel.DataAnnotations;

    public class CreateScrapBookInputModel
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Display(Name = "Cover Image")]
        public string CoverUrl { get; set; }
    }
}
