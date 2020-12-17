namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public ScrapBooksService(
            IDeletableEntityRepository<ScrapBook> scrapBooksRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Page> pagesRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.scrapBooksRepository = scrapBooksRepository;
            this.usersRepository = usersRepository;
            this.pagesRepository = pagesRepository;
            this.categoriesRepository = categoriesRepository;
            this.commentsRepository = commentsRepository;
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

        public int GetAllScrapBooksCount()
        {
            return this.scrapBooksRepository.All().Count();
        }

        public ICollection<ScrapBookViewModel> GetAllScrapBooksForUser(string userId)
        {
            var scrapBooksDb = this.scrapBooksRepository.All().Where(x => x.CreatorId == userId).ToList();
            var userDbModel = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);
            var resultScrapBooks = new List<ScrapBookViewModel>();

            foreach (var scrapBookDbModel in scrapBooksDb)
            {
                var scrapBook = new ScrapBookViewModel
                {
                    Id = scrapBookDbModel.Id,
                    CreatorId = scrapBookDbModel.CreatorId,
                    CreatorName = userDbModel.Email,
                    Name = scrapBookDbModel.Name,
                    Description = scrapBookDbModel.Description,
                    CoverUrl = scrapBookDbModel.CoverUlr,
                    IsDeleted = scrapBookDbModel.IsDeleted,
                    CreateTime = scrapBookDbModel.CreatedOn,
                };

                resultScrapBooks.Add(scrapBook);
            }

            return resultScrapBooks;
        }

        public ScrapBookViewModel GetScrapBookById(int scrapBookId)
        {
            var scrapBookDbModel = this.scrapBooksRepository.All().FirstOrDefault(x => x.Id == scrapBookId);
            var userDbModel = this.usersRepository.All().FirstOrDefault(x => x.Id == scrapBookDbModel.CreatorId);

            var viewModel = new ScrapBookViewModel
            {
                Id = scrapBookDbModel.Id,
                Name = scrapBookDbModel.Name,
                Description = scrapBookDbModel.Description,
                CoverUrl = scrapBookDbModel.CoverUlr,
                CreatorId = userDbModel.Id,
                CategoryId = scrapBookDbModel.CategoryId,
                CreatorName = userDbModel.Email,
                CategoryName = this.categoriesRepository.All().Where(x => x.Id == scrapBookDbModel.CategoryId).FirstOrDefault().Name,
                Visibility = scrapBookDbModel.Visibility,
                CreateTime = scrapBookDbModel.CreatedOn,
                PagesCount = this.pagesRepository.All().Where(x => x.ScrapBookId == scrapBookId).Count(),
                Comments = this.commentsRepository.All().Where(x => x.ScrapBookId == scrapBookId).
                Select(c => new ScrapBookCommentViewModel
                {
                    Id = c.Id,
                    OwnerName = this.usersRepository.All().FirstOrDefault(o => o.Id == c.OwnerId).UserName,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    ParentId = c.ParentId,
                }).ToList(),
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
                TotalBookPagesCount = this.pagesRepository.All().Where(x => x.ScrapBookId == scrapBookId).Count(),
                Pages = pagesDbModel.Select(x => new PageViewModel
                {
                    PageNumber = x.PageNumber,
                    Content = x.Content,
                }).ToList(),
            };

            return viewModel;
        }

        public async Task UpdateAsync(int id, EditScrapBookInputModel input)
        {
            var scrapBook = this.scrapBooksRepository.All().FirstOrDefault(x => x.Id == id);
            scrapBook.Name = input.Name;
            scrapBook.Description = input.Description;
            scrapBook.CoverUlr = input.CoverUrl;
            scrapBook.CategoryId = input.CategoryId;
            scrapBook.Visibility = input.Visibility;

            await this.scrapBooksRepository.SaveChangesAsync();
        }
    }
}
