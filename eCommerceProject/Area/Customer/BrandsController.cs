using eCommerceProject.BLL.Service.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.PL.Area.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class BrandsController : ControllerBase
    {
        private readonly BrandService _brandService;

        public BrandsController(BrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("byIdCustomer")]
        public IActionResult Get(int id)
        {
            var brand = _brandService.GetById(id);
            if (brand is null)
            {
                return NotFound();
            }
            return Ok(brand);
        }
        [HttpGet]
        public IActionResult GetAll() => Ok(_brandService.GetAll(true));
    }
}
