namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Pages;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public interface IScrapBooksService
    {
        Task CreateAsync(CreateScrapBookInputModel input, string creatorId);

        ICollection<ScrapBookViewModel> GetAllScrapBooksForUser(string userId);

        ScrapBookPagesViewModel GetScrapBookById(int scrapBookId);
    }
}
