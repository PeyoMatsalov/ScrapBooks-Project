namespace ScrapBookProject.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ScrapBookProject.Web.ViewModels.Pages;

    public interface IPagesService
    {
        Task CreateAsync(CreatePageInputModel input, int bookId);

        ICollection<PageViewModel> GetPagesByBookId(int bookId);

        int GetPagesCountByBookId(int bookId);

        IEnumerable<PageViewModel> GetCurrentPages(int bookId, int page, int pagesPerPage);

        int GetAllPagesCount();
    }
}
