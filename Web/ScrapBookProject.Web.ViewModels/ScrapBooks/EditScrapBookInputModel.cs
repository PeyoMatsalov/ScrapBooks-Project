﻿namespace ScrapBookProject.Web.ViewModels.ScrapBooks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ScrapBookProject.Web.ViewModels.Categories;

    public class EditScrapBookInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [MinLength(1, ErrorMessage = "The name must be between 1 and 30 characters.")]
        [MaxLength(30, ErrorMessage = "The name must be between 1 and 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        [MinLength(10, ErrorMessage = "The description must be between 10 and 200 characters.")]
        [MaxLength(200, ErrorMessage = "The description must be between 10 and 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose who can see your scrapbook.")]
        public string Visibility { get; set; }

        public string CoverUrl { get; set; }

        [Required(ErrorMessage = "Please choose a category for your scrapbook.")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}