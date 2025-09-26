using eCommerceProject.BLL.Service.Classes;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.PL.Area.Admin
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BrandsController : ControllerBase
    {
        private readonly BrandService _brandService;

        public BrandsController(BrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet("byId")]
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
        public IActionResult GetAll() => Ok(_brandService.GetAll());
        //[HttpPost("CreateBrand")]
        //public async Task<IActionResult> Create([FromForm] BrandRequest request)
        //{
        //    var result = await _brandService.CreateFile(request);
        //    return Ok(result);

        //}
        [HttpPost]
        public IActionResult Create([FromForm] BrandRequest request)
        {
            var id = _brandService.CreateFile(request);
            return CreatedAtAction(nameof(Get), new { id }, new { message = request });
        }
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromForm] BrandRequest request)
        {
            var isUpdated = _brandService.Update(id, request);

            return isUpdated > 0 ? Ok() : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _brandService.Delete(id);
            return isDeleted > 0 ? Ok() : NotFound();
        }
        [HttpDelete("DeleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                _brandService.DeleteAll();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
           
        }
        [HttpPatch("ChangeStatus/{id}")]
        public IActionResult ChangeStatus(int id)
        {
            var isChanged = _brandService.ToggleStatus(id);
            return isChanged ? Ok() : NotFound();
        }

    }
}

