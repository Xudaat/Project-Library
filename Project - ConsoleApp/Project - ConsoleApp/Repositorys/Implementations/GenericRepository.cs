
using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp.Data;
using Project___ConsoleApp.Models;
using Project___ConsoleApp.Repository.Interface;

namespace Project___ConsoleApp.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _appDbContext;

        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public void Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);

        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public List<T> GetAll()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _appDbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }
    }
}
