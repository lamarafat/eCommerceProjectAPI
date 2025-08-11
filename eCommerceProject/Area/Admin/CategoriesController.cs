using eCommerceProject.BLL.Service.Classes;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.PL.Area.Admin
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var category = _categoryService.GetById(id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpGet]
        public IActionResult GetAll() => Ok(_categoryService.GetAll());

    }
}
