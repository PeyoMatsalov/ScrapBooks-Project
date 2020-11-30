namespace ScrapBookProject.Web.ViewModels.Pages
{
    public class LeftPageViewModel
    {
        public int BookId { get; set; }

        public int Number { get; set; }

        public string Content { get; set; }

        public bool HasPreviousPage => this.Number > 0;

        public int PreviousPageNumber => this.Number - 1;
    }
}
