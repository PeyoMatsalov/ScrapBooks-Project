namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Pages;

    public class PagesService : IPagesService
    {
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;
        private readonly IDeletableEntityRepository<Page> pagesRepository;

        public PagesService(IDeletableEntityRepository<ScrapBook> scrapBooksRepository, IDeletableEntityRepository<Page> pagesRepository)
        {
            this.scrapBooksRepository = scrapBooksRepository;
            this.pagesRepository = pagesRepository;
        }

        public async Task CreateAsync(CreatePageInputModel input, int bookId)
        {
            var pages = this.pagesRepository.All().Where(x => x.ScrapBookId == bookId).ToList();

            int pageNumber = pages.Last().PageNumber;

            Page page = new Page
            {
                ScrapBookId = bookId,
                Content = input.Content,
                PageNumber = ++pageNumber,
            };

            await this.pagesRepository.AddAsync(page);
            await this.pagesRepository.SaveChangesAsync();
        }

        public ICollection<PageViewModel> GetPagesByBookId(int bookId)
        {
            ICollection<PageViewModel> pages = new List<PageViewModel>();

            foreach (var book in this.pagesRepository.All().Where(x => x.ScrapBookId == bookId))
            {
                PageViewModel page = new PageViewModel
                {
                    BookId = bookId,
                    Content = book.Content,
                    Number = book.PageNumber,
                    ImageUrl = book.ImageUrl,
                };

                pages.Add(page);
            }

            return pages;
        }
    }
}
