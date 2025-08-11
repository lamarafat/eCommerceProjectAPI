using eCommerceProject.BLL.Service.Classes;
using eCommerceProject.BLL.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.PL.Area.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("byIdCustomer")]
        public IActionResult Get(int id)
        {
            var brand = _categoryService.GetById(id);
            if (brand is null)
            {
                return NotFound();
            }
            return Ok(brand);
        }
        [HttpGet]
        public IActionResult GetAll() => Ok(_categoryService.GetAll(true));
    }
}
