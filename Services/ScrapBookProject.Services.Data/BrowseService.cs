namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class BrowseService : IBrowseService
    {
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;

        public BrowseService(IDeletableEntityRepository<ScrapBook> scrapBooksRepository)
        {
            this.scrapBooksRepository = scrapBooksRepository;
        }

        public ICollection<ScrapBookViewModel> GetAllPublicScrapBooks()
        {
            var scrapBooksDb = this.scrapBooksRepository.All().Where(x => x.Visibility == "Public" && x.IsDeleted == false).ToList();
            var resultScrapBooks = new List<ScrapBookViewModel>();

            foreach (var scrapBookDbModel in scrapBooksDb)
            {
                var scrapBook = new ScrapBookViewModel
                {
                    Id = scrapBookDbModel.Id,
                    Name = scrapBookDbModel.Name,
                    Description = scrapBookDbModel.Description,
                    CoverUrl = scrapBookDbModel.CoverUlr,
                    IsDeleted = scrapBookDbModel.IsDeleted,
                };

                resultScrapBooks.Add(scrapBook);
            }

            return resultScrapBooks;
        }
    }
}
