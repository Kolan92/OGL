namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repository.Models.ApplicationDbContext context)
        {
            //For debug purpose
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();


            SeedRoles(context);
            SeedUsers(context);
            SeedAdvertistments(context);
            SeedCategory(context);
            SeedAdvertistmentCategory(context);

        }

        private void SeedAdvertistmentCategory(ApplicationDbContext context)
        {
            if (context.AdvertistmentCategory.Count() ==0)
            {
                for (int i = 0; i < 10; i++)
                {
                    var advertistmentCategory = new AdvertistmentCategory()
                    {
                        Id = i,
                        AdvertistmentId = i / 2 + 1,
                        CategoryId = i / 2 + 2
                    };
                    context.Set<AdvertistmentCategory>().AddOrUpdate(advertistmentCategory);
                }
                context.SaveChanges();
            }
        }

        private void SeedCategory(ApplicationDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var category = new Category()
                {
                    Id = i,
                    Name = string.Format("Category {0} name", i.ToString()),
                    Text = string.Format("Advertistment {0} text", i.ToString()),
                    MetaTitle = string.Format("Title {0}", i.ToString()),
                    MetaDescription = i.ToString(),
                    MetaKeyWords = i.ToString(),
                    ParenId = i
                };
                context.Set<Category>().AddOrUpdate(category);
            }
            context.SaveChanges();
        }

        private void SeedAdvertistments(ApplicationDbContext context)
        {
            var userId = context.Set<ApplicationUser>()
                                                .Where(u => u.UserName == "Admin")
                                                .FirstOrDefault().Id;

            for (int i = 0; i < 10; i++)
            {
                var advertistment = new Advertisement()
                {
                    Id = i,
                    UserID = userId,
                    AdvertisementText = string.Format("Advertistment {0} text", i.ToString()),
                    AdvertistmentTitle = string.Format("Advertistmetn {0} title", i.ToString()),
                    PublishDate = DateTime.Today.AddDays(-1)
                };
                context.Set<Advertisement>().AddOrUpdate(advertistment);
            }
            context.SaveChanges();
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var user = new ApplicationUser { UserName = "Admin" };
                var adminresult = manager.Create(user, "1234Abc.");

                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName.StartsWith("User")))
            {
                for (int i = 0; i < 10; i++)
                {
                    var user = new ApplicationUser { UserName = string.Format("User{0}@gmail.com", i.ToString()) };
                    var adminResult = manager.Create(user, "1234Abc.");
                    if (adminResult.Succeeded)
                        manager.AddToRole(user.Id, "Worker");
                }
            }
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if(!roleManager.RoleExists("Worker"))
            {
                var role = new IdentityRole();
                role.Name = "Worker";
                roleManager.Create(role);
            }
        }
    }
}
