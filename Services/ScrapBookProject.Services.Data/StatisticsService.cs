namespace ScrapBookProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Administration;

    public class StatisticsService : IStatisticsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public StatisticsService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public SiteStatisticsViewModel GetRegisteredUserCountForPast3Months()
        {
            var firstMonth = new MonthViewModel
            {
                Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-2).Month),
                RegisteredUsersCount = this.usersRepository.All().Where(x => x.CreatedOn.Month == DateTime.Now.AddMonths(-2).Month).Count(),
            };

            var secondMonth = new MonthViewModel
            {
                Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-1).Month),
                RegisteredUsersCount = this.usersRepository.All().Where(x => x.CreatedOn.Month == DateTime.Now.AddMonths(-1).Month).Count(),
            };

            var thirdMonth = new MonthViewModel
            {
                Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month),
                RegisteredUsersCount = this.usersRepository.All().Where(x => x.CreatedOn.Month == DateTime.Now.Month).Count(),
            };

            var result = new SiteStatisticsViewModel();
            result.MonthData.Add(firstMonth);
            result.MonthData.Add(secondMonth);
            result.MonthData.Add(thirdMonth);

            return result;
        }

        public int GetRegisteredUsersCount()
        {
            return this.usersRepository.All().Count();
        }
    }
}
