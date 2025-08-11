using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface IGenericService<TRequest, TResponse, TEntity>
    where TEntity : BaseModel
    {
        //int Create(TRequest request);
        //int Delete(int id);
        //void DeleteAll();
        //IEnumerable<TResponse> GetAll(bool onlyActive = false);
        //TResponse GetById(int id);
        //int Update(int id, TRequest request);
        //bool ToggleStatus(int id);

        int Create(TRequest request);
        int Delete(int id);
        void DeleteAll();
        IEnumerable<TResponse> GetAll(bool onlyActive = false);
        TResponse GetById(int id);
        int Update(int id, TRequest request);
        bool ToggleStatus(int id);
    }
}
