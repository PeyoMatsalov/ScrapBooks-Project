namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public interface IScrapBooksService
    {
        Task CreateAsync(CreateScrapBookInputModel input);

        ICollection<ScrapBookViewModel> GetAllScrapBooks();
    }
}
