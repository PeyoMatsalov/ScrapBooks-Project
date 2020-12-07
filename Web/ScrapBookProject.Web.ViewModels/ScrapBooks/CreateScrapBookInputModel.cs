namespace ScrapBookProject.Web.ViewModels.ScrapBooks
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ScrapBookProject.Web.ViewModels.Categories;

    public class CreateScrapBookInputModel
    {
        [Required(ErrorMessage = "The name is required.")]
        [MinLength(1, ErrorMessage = "The name must be between 1 and 30 characters.")]
        [MaxLength(30, ErrorMessage = "The name must be between 1 and 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        [MinLength(10, ErrorMessage = "The description must be between 10 and 500 characters.")]
        [MaxLength(500, ErrorMessage = "The description must be between 10 and 500 characters.")]
        public string Description { get; set; }

        public string CoverUrl { get; set; }

        [Required(ErrorMessage = "Please choose who can see your scrapbook.")]
        public string Visibility { get; set; }

        [Required(ErrorMessage = "Please choose a category for your scrapbook.")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}
