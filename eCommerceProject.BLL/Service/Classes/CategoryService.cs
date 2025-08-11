using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.Model.Category;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model.Category;
using Mapster;

namespace eCommerceProject.BLL.Service.Classes
{
    public class CategoryService : GenericService<CategoryRequestDto, CategoryResponseDto, CategoryAlt>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) :base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
      

       
    }
}
