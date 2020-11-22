namespace ScrapBookProject.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ScrapBookProject.Web.ViewModels.Pages;

    public interface IPagesService
    {
        Task CreateAsync(CreatePageInputModel input);

        ICollection<PageViewModel> GetPagesByBookId(int bookId);
    }
}
