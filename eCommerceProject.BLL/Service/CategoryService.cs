using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Repositories;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model.Category;
using Mapster;

namespace eCommerceProject.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
      

        public int CreateCategory(CategoryRequestDto request)
        {
            var category = request.Adapt<Category>();
            return _categoryRepository.Add(category);
        }


        public void DeleteAll()
        {
            _categoryRepository.DeleteAll();
        }

        public int DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null )return 0; 
            return _categoryRepository.Delete(id);
        }


        public IEnumerable<CategoryResponseDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return categories.Adapt<IEnumerable<CategoryResponseDto>>();
        }

        public CategoryResponseDto GetById(int id)
        {
           var category = _categoryRepository.GetById(id);
            return category is null ? null : category.Adapt<CategoryResponseDto>();
        }


        public IEnumerable<CategoryResponseDto> GetByLang(string lang = "en")
        {
            var categories = _categoryRepository.GetAllByLang(lang);

            return categories.Select(cat => new CategoryResponseDto
            {
                Id = cat.Id,
                CategoryTranslations = new List<CategoryTranslationResponse>
        {
            new CategoryTranslationResponse
            {
                Name = cat.CategoryTranslations.FirstOrDefault(t => t.Language == lang)?.Name,
                Language = lang
            }
        }
            });
        }




        public bool ToggleStatus(int id)
        {
            return _categoryRepository.ToggleStatus(id);
        }

        public int UpdateCategory(int id, CategoryRequestDto request)
        {
            var translations = request.CategoryTranslations?.Adapt<List<CategoryTranslation>>();
            return _categoryRepository.Update(id, translations);
        }

       
    }
}
