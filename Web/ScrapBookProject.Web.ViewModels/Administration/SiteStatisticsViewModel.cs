namespace ScrapBookProject.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    public class SiteStatisticsViewModel
    {
        public SiteStatisticsViewModel()
        {
            this.MonthData = new HashSet<MonthViewModel>();
        }

        public ICollection<MonthViewModel> MonthData { get; set; }
    }
}
