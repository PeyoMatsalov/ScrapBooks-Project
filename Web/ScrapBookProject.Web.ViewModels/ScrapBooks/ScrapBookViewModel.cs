namespace ScrapBookProject.Web.ViewModels.ScrapBooks
{
    using System.Collections.Generic;

    using ScrapBookProject.Web.ViewModels.Categories;

    public class ScrapBookViewModel
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public bool IsDeleted { get; set; }

        public string Visibility { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}
