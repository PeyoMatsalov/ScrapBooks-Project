namespace ScrapBookProject.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int ScrapBookId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
