namespace ScrapBookProject.Services.Data
{
    using ScrapBookProject.Web.ViewModels.Administration;

    public interface IStatisticsService
    {
        SiteStatisticsViewModel GetRegisteredUserCountForPast3Months();

        int GetRegisteredUsersCount();

        int GetUserCountInAgeRange(int firstYears, int secondYears);
    }
}
