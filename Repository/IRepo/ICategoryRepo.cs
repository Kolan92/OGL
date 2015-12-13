using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface ICategoryRepo
    {
        IQueryable<Category> GetCategories();
        IQueryable<Advertisement> GetAdvertistmensForCategory(int id);
        string GetCategoryNameById(int id);
    }
}
