namespace ScrapBookProject.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class EditCategoryInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [MinLength(1, ErrorMessage = "The name must be between 1 and 15 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The image URL is required.")]
        public string ImgUrl { get; set; }
    }
}
