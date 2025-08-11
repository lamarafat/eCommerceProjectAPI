using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.Model;

namespace eCommerceProject.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        int Add(T entity);
        int Delete(int id);
        void DeleteAll();
        IEnumerable<T> GetAll(bool withTracking = false);
        T GetById(int id);
        int Update(T entity);
        bool ToggleStatus(int id);
    }
}
