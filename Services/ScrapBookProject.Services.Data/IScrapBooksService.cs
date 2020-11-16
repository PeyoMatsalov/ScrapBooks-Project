namespace ScrapBookProject.Services.Data
{
    using System.Threading.Tasks;

    using ScrapBookProject.Web.ViewModels.ScrapBooks;

    public interface IScrapBooksService
    {
        Task CreateAsync(CreateScrapBookInputModel input);
    }
}
