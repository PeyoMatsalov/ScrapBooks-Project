namespace ScrapBookProject.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateCommentInputModel
    {
        public int ScrapBookId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
