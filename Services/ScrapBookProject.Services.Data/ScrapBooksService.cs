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
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public class ScrapBooksService : IScrapBooksService
    {
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Page> pagesRepository;

        public ScrapBooksService(IDeletableEntityRepository<ScrapBook> scrapBooksRepository, IDeletableEntityRepository<ApplicationUser> usersRepository, IDeletableEntityRepository<Page> pagesRepository)
        {
            this.scrapBooksRepository = scrapBooksRepository;
            this.usersRepository = usersRepository;
            this.pagesRepository = pagesRepository;
        }

        public async Task CreateAsync(CreateScrapBookInputModel input, string userId)
        {
            ApplicationUser user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            var scrapBook = new ScrapBook
            {
                Name = input.Name,
                Description = input.Description,
                CoverUlr = input.CoverUrl,
                Visibility = input.Visibility,
                CategoryId = input.CategoryId,
            };

            var page = new Page
            {
                PageNumber = 0,
                Content = "Page 0",
                ImageUrl = string.Empty,
            };

            scrapBook.Pages.Add(page);
            user.ScrapBooks.Add(scrapBook);

            await this.scrapBooksRepository.AddAsync(scrapBook);
            await this.scrapBooksRepository.SaveChangesAsync();
        }

        public async Task DeleteScrapBookAsync(int scrapBookId)
        {
            var book = this.scrapBooksRepository.All().FirstOrDefault(x => x.Id == scrapBookId);
            book.IsDeleted = true;
            this.scrapBooksRepository.Update(book);
            await this.scrapBooksRepository.SaveChangesAsync();
        }

        public ICollection<ScrapBookViewModel> GetAllScrapBooksForUser(string userId)
        {
            var scrapBooksDb = this.scrapBooksRepository.All().Where(x => x.CreatorId == userId).ToList();
            var resultScrapBooks = new List<ScrapBookViewModel>();

            foreach (var scrapBookDbModel in scrapBooksDb)
            {
                var scrapBook = new ScrapBookViewModel
                {
                    Id = scrapBookDbModel.Id,
                    CreatorId = scrapBookDbModel.CreatorId,
                    Name = scrapBookDbModel.Name,
                    Description = scrapBookDbModel.Description,
                    CoverUrl = scrapBookDbModel.CoverUlr,
                    IsDeleted = scrapBookDbModel.IsDeleted,
                };

                resultScrapBooks.Add(scrapBook);
            }

            return resultScrapBooks;
        }

        public ScrapBookViewModel GetScrapBookById(int scrapBookId)
        {
            var scrapBookDbModel = this.scrapBooksRepository.All().FirstOrDefault(x => x.Id == scrapBookId);

            var viewModel = new ScrapBookViewModel
            {
                Id = scrapBookDbModel.Id,
                Name = scrapBookDbModel.Name,
                Description = scrapBookDbModel.Description,
                CoverUrl = scrapBookDbModel.CoverUlr,
            };

            return viewModel;
        }

        public ScrapBookPagesViewModel GetScrapBookWithPagesById(int scrapBookId)
        {
            var scrapBookDbModel = this.scrapBooksRepository.All().FirstOrDefault(x => x.Id == scrapBookId);
            var pagesDbModel = this.pagesRepository.All().Where(x => x.ScrapBookId == scrapBookId).ToList();

            var viewModel = new ScrapBookPagesViewModel
            {
                Id = scrapBookDbModel.Id,
                CreatorId = scrapBookDbModel.CreatorId,
                Name = scrapBookDbModel.Name,
                Description = scrapBookDbModel.Description,
                CoverUrl = scrapBookDbModel.CoverUlr,
                Pages = pagesDbModel.Select(x => new PageViewModel
                {
                    Number = x.PageNumber,
                    Content = x.Content,
                }).ToList(),
            };

            return viewModel;
        }
    }
}
