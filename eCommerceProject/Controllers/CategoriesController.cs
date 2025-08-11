using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.Repositories;
using eCommerceProject.Data;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model;
using eCommerceProject.Model.Category;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace eCommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IStringLocalizer<SharedResource> localizer, ICategoryService categoryService)
        {
            _localizer = localizer;
            _categoryService = categoryService;
        }
        
       
        [HttpGet("{id}")]
        public IActionResult Details([FromRoute]int id)
        {

            var category = _categoryService.GetById(id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            return Ok(category.Adapt<CategoryResponseDto>());
        }
        [HttpPost]
        public IActionResult Create([FromBody]CategoryRequestDto request)
        {
            var category = request.Adapt<Category>();
            _categoryService.Create(request);
            return Ok(new { message = _localizer["Created"].Value });
        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequestDto request)
        {
            //category.Status = request.Status; عملتها كومنت لانه انا مش حاطة في ال requestDto  اشي لل status  بس في حال حطيت او حطيت نوع تاني بكتبهم بهاي الطريقة طالما مش موجودين جوا translation ال  
            try
            {
                var translation = request.CategoryTranslations.Adapt<List<CategoryTranslation>>();
                _categoryService.Update(id, request);
                return Ok(new { message = _localizer["Updated"].Value });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });


            }
        }

        [HttpPatch("{id}/toggleStatus")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var status = _categoryService.ToggleStatus(id);
            if (!status)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            else
            {
                return Ok(new { message = _localizer["Success"].Value });
            }
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]string lang = "en")
        {
            var result = _categoryService.GetAll();
            if (result is null || !result.Any())
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }

            return Ok(new { message = _localizer["Success"].Value, data = result });
        }
        [HttpGet("all")]
        public IActionResult GetAllCategories()
        {
            var cats = _categoryService.GetAll().Adapt<List<CategoryResponseDto>>();
            return Ok(new { message = _localizer["Success"].Value, cats });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            try
            {
               var delete =  _categoryService.Delete(id);
                return Ok(new { message = _localizer["Deleted"].Value });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            
        }
        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll()
        {

            try
            {
                _categoryService.DeleteAll();
                return Ok(new { message = _localizer["Deleted"].Value });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = _localizer["Empty"].Value });
            }
            
        }
        

    }
}
