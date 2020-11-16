namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
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
    }
}
