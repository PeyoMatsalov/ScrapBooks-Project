namespace ScrapBookProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(
            IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int scrapbookId, string ownerId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                OwnerId = ownerId,
                ScrapBookId = scrapbookId,
            };
            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInScrapBookId(int commentId, int scrapBookId)
        {
            var commentScrapBookId = this.commentsRepository.All().Where(x => x.Id == commentId)
                .Select(x => x.ScrapBookId).FirstOrDefault();
            return commentScrapBookId == scrapBookId;
        }
    }
}
