using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IAdvertistmentRepo
    {
        IQueryable<Advertisement> GetAdvertistments();
        Advertisement GetAdvertistmentById(int Id);
        void RemoveAdvertistment(int id);
        void SaveChanges();
        void Create(Advertisement advertistment);
        void UpdateAdvertistment(Advertisement advertistmetn);

    }
}
