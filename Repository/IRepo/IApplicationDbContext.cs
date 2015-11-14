using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepo
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Cotegories { get; set; }
        DbSet<Advertisement> Advertistments { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<AdvertistmentCategory> AdvertistmentCategory { get; set; }
        DbEntityEntry Entry(object entity);

        int SaveChanges();
        Database Database { get; }
    }
}
