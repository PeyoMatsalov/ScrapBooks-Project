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

        ScrapBookViewModel GetScrapBookById(int scrapBookId);

        ScrapBookPagesViewModel GetScrapBookWithPagesById(int scrapBookId);

        Task DeleteScrapBookAsync(int scrapBookId);

        Task UpdateAsync(int id, EditScrapBookInputModel input);
    }
}
