namespace ScrapBookProject.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateCommentAsync(int scrapBookId, string ownerId, string content, int? parentId = null);

        bool IsInScrapBookId(int commentId, int scrapBookId);
    }
}
