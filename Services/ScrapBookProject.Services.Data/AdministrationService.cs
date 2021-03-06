﻿namespace ScrapBookProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Common.Repositories;
    using ScrapBookProject.Data.Models;
    using ScrapBookProject.Web.ViewModels.Administration;

    public class AdministrationService : IAdministrationSevice
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<ScrapBook> scrapBooksRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AdministrationService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<ScrapBook> scrapBooksRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.scrapBooksRepository = scrapBooksRepository;
            this.userRepository = userRepository;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            user.IsDeleted = true;
            await this.userRepository.SaveChangesAsync();
        }

        public ICollection<UserViewModel> GetAllUsers()
        {
            return this.userRepository.All().Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                DateJoined = x.CreatedOn.ToString("dd-MM-y"),
            }).ToList();
        }

        public ICollection<UserViewModel> GetUserInfo(string searchBy, string searchValue)
        {
            List<UserViewModel> result = new List<UserViewModel>();
            if (searchBy == "Name")
            {
                result = this.userRepository.All().Where(x => x.UserName.Contains(searchValue) || searchValue == null).Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                    Id = x.Id,
                    DateJoined = x.CreatedOn.ToString("dd-MM-y"),
                }).ToList();
            }
            else
            {
                result = this.userRepository.All().Where(x => x.Id == searchValue).Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                    Id = x.Id,
                    DateJoined = x.CreatedOn.ToString("dd-MM-y"),
                }).ToList();
            }

            return result;
        }
    }
}
