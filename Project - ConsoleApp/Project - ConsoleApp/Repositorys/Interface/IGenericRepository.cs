using Project___ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Repository.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        void Create(T entity);
        void Commit();
        List<T> GetAll();
        void Delete(T entity);
         T GetById(int id);
    }
}
