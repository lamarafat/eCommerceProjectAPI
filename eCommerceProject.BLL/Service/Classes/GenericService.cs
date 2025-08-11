//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using eCommerceProject.BLL.Service.Interfaces;
//using eCommerceProject.DAL.Repositories.Interfaces;
//using eCommerceProject.Model;
//using Mapster;

//namespace eCommerceProject.BLL.Service.Classes
//{
//    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
//    {
//        private readonly IGenericRepository<TEntity> _genericRepository;

//        public GenericService(IGenericRepository<TEntity> genericRepository)
//        {
//            _genericRepository = genericRepository;
//        }

//        public int Create(TRequest request)
//        {
//            var entity = request.Adapt<TEntity>();
//            return _genericRepository.Add(entity);
//        }

//        public int Delete(int id)
//        {
//            return _genericRepository.Delete(id);
//        }

//        public void DeleteAll()
//        {
//            _genericRepository.DeleteAll();
//        }

//        public IEnumerable<TResponse> GetAll()
//        {
//            var entities = _genericRepository.GetAll();
//            return entities.Adapt<IEnumerable<TResponse>>();
//        }

//        public TResponse GetById(int id)
//        {
//            var entity = _genericRepository.GetById(id);
//            return entity is null ? default : entity.Adapt<TResponse>();
//        }

//        public int Update(int id, TRequest request)
//        {
//            var entity = request.Adapt<TEntity>();
//            entity.Id = id;
//            return _genericRepository.Update(entity);
//        }

//        public bool ToggleStatus(int id)
//        {
//            return _genericRepository.ToggleStatus(id);
//        }
//    }
//}



using System.Collections.Generic;
using System.Linq;
using Mapster;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Model;

namespace eCommerceProject.BLL.Service.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity>
        where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _genericRepository.Add(entity);
        }

        public int Delete(int id) => _genericRepository.Delete(id);

        public void DeleteAll() => _genericRepository.DeleteAll();

        public IEnumerable<TResponse> GetAll(bool onlyActive = false)
        {
            var entities = _genericRepository.GetAll();
            if (onlyActive)
            {
                entities = entities.Where(e => e.Status == Status.Active);
            }
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse GetById(int id)
        {
            var entity = _genericRepository.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public int Update(int id, TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            entity.Id = id;
            return _genericRepository.Update(entity);
        }

        public bool ToggleStatus(int id) => _genericRepository.ToggleStatus(id);
    }
}

