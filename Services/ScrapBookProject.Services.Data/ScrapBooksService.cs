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

        public ScrapBooksService(IDeletableEntityRepository<ScrapBook> scrapBooksRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.scrapBooksRepository = scrapBooksRepository;
            this.usersRepository = usersRepository;
        }

        public async Task CreateAsync(CreateScrapBookInputModel input, string userId)
        {
            ApplicationUser user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            var scrapBook = new ScrapBook
            {
                Name = input.Name,
                Description = input.Description,
                CoverUlr = input.CoverUrl,
            };

            user.ScrapBooks.Add(scrapBook);

            await this.scrapBooksRepository.AddAsync(scrapBook);
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
                    Name = scrapBookDbModel.Name,
                    Description = scrapBookDbModel.Description,
                    CoverUrl = scrapBookDbModel.CoverUlr,
                };

                resultScrapBooks.Add(scrapBook);
            }

            return resultScrapBooks;
        }

        public ScrapBookPagesViewModel GetScrapBookById(int scrapBookId)
        {
            var scrapBookDbModel = this.scrapBooksRepository.All().FirstOrDefault(x => x.Id == scrapBookId);
            ScrapBookPagesViewModel viewModel = new ScrapBookPagesViewModel
            {
                Id = scrapBookDbModel.Id,
                Name = scrapBookDbModel.Name,
                Description = scrapBookDbModel.Description,
                Pages = scrapBookDbModel.Pages,
                CoverUrl = scrapBookDbModel.CoverUlr,
            };

            return viewModel;
        }
    }
}
