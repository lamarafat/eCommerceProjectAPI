using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;

namespace eCommerceProject.BLL.Service
{
    public interface ICategoryService
    {
        int CreateCategory(CategoryRequestDto request);
        int DeleteCategory(int id);
        void DeleteAll();
        IEnumerable<CategoryResponseDto> GetAllCategories();
        CategoryResponseDto GetById(int id);
        IEnumerable<CategoryResponseDto> GetByLang(string lang = "en");
        int UpdateCategory(int id,CategoryRequestDto request);
        bool ToggleStatus(int id);
    }
}
