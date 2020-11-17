namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class ScrapBooksService : IScrapBooksService
    {
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;

        public ScrapBooksService(IDeletableEntityRepository<ScrapBook> scrapBooksRepository)
        {
            this.scrapBooksRepository = scrapBooksRepository;
        }

        public async Task CreateAsync(CreateScrapBookInputModel input)
        {
            var scrapBook = new ScrapBook
            {
                Name = input.Name,
                Description = input.Description,
                CoverUlr = input.CoverUrl,
            };

            await this.scrapBooksRepository.AddAsync(scrapBook);
            await this.scrapBooksRepository.SaveChangesAsync();
        }

        public ICollection<ScrapBookViewModel> GetAllScrapBooks()
        {
            var scrapBooksDb = this.scrapBooksRepository.All().ToList();
            var resultScrapBooks = new List<ScrapBookViewModel>();

            foreach (var scrapBookDbModel in scrapBooksDb)
            {
                var scrapBook = new ScrapBookViewModel
                {
                    Name = scrapBookDbModel.Name,
                    Description = scrapBookDbModel.Description,
                    CoverUrl = scrapBookDbModel.CoverUlr,
                };

                resultScrapBooks.Add(scrapBook);
            }

            return resultScrapBooks;
        }
    }
}
