namespace ScrapBookProject.Services.Data
{
    using ScrapBookProject.Web.ViewModels.Administration;

    public interface IStatisticsService
    {
        public SiteStatisticsViewModel GetRegisteredUserCountForPast3Months();
    }
}
