namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Pages;

    public class PagesService : IPagesService
    {
        private readonly IDeletableEntityRepository<Page> pagesRepository;

        public PagesService(IDeletableEntityRepository<Page> pagesRepository)
        {
            this.pagesRepository = pagesRepository;
        }

        public async Task CreateAsync(CreatePageInputModel input, int bookId)
        {
            var pages = this.pagesRepository.All().Where(x => x.ScrapBookId == bookId).ToList();

            int pageNumber = pages.Any() ? pages.Last().PageNumber : 0;

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
                    PageNumber = book.PageNumber,
                };

                pages.Add(page);
            }

            return pages;
        }

        public int GetPagesCountByBookId(int bookId)
        {
            return this.pagesRepository.All().Where(x => x.ScrapBookId == bookId).Count();
        }

        public IEnumerable<PageViewModel> GetCurrentPages(int bookId, int pageNumber, int pagesPerPage)
        {
            return this.pagesRepository.All()
                .Where(x => x.ScrapBookId == bookId)
                .OrderBy(x => x.PageNumber)
                .Skip((pageNumber - 1) * pagesPerPage)
                .Take(pagesPerPage)
                .Select(x => new PageViewModel
                {
                    BookName = x.ScrapBook.Name,
                    BookId = x.ScrapBookId,
                    PageNumber = x.PageNumber,
                    Content = x.Content,
                });
        }

        public int GetAllPagesCount()
        {
            return this.pagesRepository.All().Count();
        }
    }
}
