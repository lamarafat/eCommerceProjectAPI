using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Data;
using eCommerceProject.Model;
using eCommerceProject.Model.Category;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseModel
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                throw new Exception("Item not found");

            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public void DeleteAll()
        {
            var items = _context.Set<T>().ToList();
            _context.RemoveRange(items);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            var query = _context.Set<T>().OrderByDescending(x => x.CreatedAt);
            return withTracking ? query.ToList() : query.AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public bool ToggleStatus(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null) return false;

            entity.Status = entity.Status == Status.Active ? Status.In_active : Status.Active;
            _context.SaveChanges();
            return true;
        }

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }

}
