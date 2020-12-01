namespace ScrapBookProject.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ScrapBookProject.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Sport",
                        Description = "",
                        ImageUrl = "https://media.npr.org/assets/img/2020/06/10/gettyimages-200199027-001-b5fb3d8d8469ab744d9e97706fa67bc5c0e4fa40.jpg",
                    },
                    new Category
                    {
                        Name = "Health",
                        Description = "",
                        ImageUrl = "https://mk0nationalecze417sw.kinstacdn.com/wp-content/uploads/2017/05/hands-with-health-.jpg",
                    },
                    new Category
                    {
                        Name = "Technology",
                        Description = "",
                        ImageUrl = "https://www.homecareinsight.co.uk/wp-content/uploads/2020/07/connected-technology.jpg",
                    },
                    new Category
                    {
                        Name = "Programming",
                        Description = "",
                        ImageUrl = "https://miro.medium.com/max/12032/0*Fu_vcP7P_uHF1Szk",
                    },
                    new Category
                    {
                        Name = "Cooking",
                        Description = "",
                        ImageUrl = "https://images.indianexpress.com/2019/07/cooking_1200.jpg",
                    },
                    new Category
                    {
                        Name = "Food",
                        Description = "",
                        ImageUrl = "https://health.clevelandclinic.org/wp-content/uploads/sites/3/2019/06/cropped-GettyImages-643764514.jpg",
                    },
                    new Category
                    {
                        Name = "Pets",
                        Description = "",
                        ImageUrl = "https://images.ctfassets.net/y5z23yb0t4f0/13ivYcFz04vy24IEMN3UNn/24cb7bd7ad4609d768a0c138f5938528/pets31.jpg",
                    },
                    new Category
                    {
                        Name = "Education",
                        Description = "",
                        ImageUrl = "https://etimg.etb2bimg.com/photo/75729614.cms",
                    },
                    new Category
                    {
                        Name = "Business",
                        Description = "",
                        ImageUrl = "https://blog.edx.org/wp-content/uploads/2016/03/Business-Management.jpg",
                    },
                    new Category
                    {
                        Name = "Nature",
                        Description = "",
                        ImageUrl = "https://cdn.pixabay.com/photo/2015/12/01/20/28/road-1072823__340.jpg",
                    },
                    new Category
                    {
                        Name = "Travel",
                        Description = "",
                        ImageUrl = "https://www.dragoninsure.com/wp-content/uploads/2019/05/coastal-family-health-travel-clinic.jpg",
                    },
                    new Category
                    {
                        Name = "Life",
                        Description = "",
                        ImageUrl = "https://miro.medium.com/max/7018/1*Y6E0MBrBaqIWekxmpQL-vg.jpeg",
                    },
                    new Category
                    {
                        Name = "Science",
                        Description = "",
                        ImageUrl = "https://www.tutorialspoint.com/fundamentals_of_science_and_technology/images/evolution_of_science.jpg",
                    },
                };
                foreach (var category in categories)
                {
                    await dbContext.Categories.AddAsync(category);
                }
            }
        }
    }
}
