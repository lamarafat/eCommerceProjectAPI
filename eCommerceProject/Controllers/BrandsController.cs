using eCommerceProject.BLL.Service;
using eCommerceProject.DAL.Repositories;
using eCommerceProject.Data;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model;
using eCommerceProject.DAL.Model.Brand;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using eCommerceProject.BLL.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using eCommerceProject.DAL.DTO.Request;

namespace eCommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IBrandService _brandService;

        public BrandsController(IStringLocalizer<SharedResource> localizer, IBrandService brandService)
        {
            _localizer = localizer;
            _brandService = brandService;
        }


        //[HttpGet("{id}")]
        //public IActionResult Details([FromRoute]int id)
        //{

        //    var brand = _brandService.GetById(id);
        //    if (brand is null)
        //    {
        //        return NotFound(new { message = _localizer["NotFound"].Value });
        //    }
        //    return Ok(brand.Adapt<BrandResponseDto>());
        //}
        //[HttpPost]
        //public IActionResult Create([FromBody]BrandRequestDto request)
        //{
        //    var brand = request.Adapt<Brand>();
        //    _brandService.Create(request);
        //    return Ok(new { message = _localizer["Created"].Value });
        //}
        //[HttpPatch("{id}")]
        //public IActionResult Update([FromRoute] int id, [FromBody] BrandRequestDto request)
        //{
        //    //brand.Status = request.Status; عملتها كومنت لانه انا مش حاطة في ال requestDto  اشي لل status  بس في حال حطيت او حطيت نوع تاني بكتبهم بهاي الطريقة طالما مش موجودين جوا translation ال  
        //    try
        //    {
        //        var translation = request.BrandTranslations.Adapt<List<BrandTranslationRequest>>();
        //        _brandService.Update(id, request);
        //        return Ok(new { message = _localizer["Updated"].Value });
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(new { message = _localizer["NotFound"].Value });


        //    }
        //}

        //[HttpPatch("{id}/toggleStatus")]
        //public IActionResult ToggleStatus([FromRoute] int id)
        //{
        //    var status = _brandService.ToggleStatus(id);
        //    if (!status)
        //    {
        //        return NotFound(new { message = _localizer["NotFound"].Value });
        //    }
        //    else
        //    {
        //        return Ok(new { message = _localizer["Success"].Value });
        //    }
        //}

        //[HttpGet(" ")]
        //public IActionResult GetAll([FromQuery]string lang = "en")
        //{
        //    var result = _brandService.GetAll();
        //    if (result is null || !result.Any())
        //    {
        //        return NotFound(new { message = _localizer["NotFound"].Value });
        //    }

        //    return Ok(new { message = _localizer["Success"].Value, data = result });
        //}
        //[HttpGet("all")]
        //public IActionResult GetAllBrands()
        //{
        //    var brands = _brandService.GetAll().Adapt<List<BrandResponseDto>>();
        //    return Ok(new { message = _localizer["Success"].Value, brands });
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete([FromRoute]int id)
        //{
        //    try
        //    {
        //       var delete = _brandService.Delete(id);
        //        return Ok(new { message = _localizer["Deleted"].Value });
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(new { message = _localizer["NotFound"].Value });
        //    }

        //}
        //[HttpDelete("deleteAll")]
        //public IActionResult DeleteAll()
        //{

        //    try
        //    {
        //        _brandService.DeleteAll();
        //        return Ok(new { message = _localizer["Deleted"].Value });
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(new { message = _localizer["Empty"].Value });
        //    }

        //}


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var brand = _brandService.GetById(id);
            if (brand is null)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            return Ok(brand);
        }
        [HttpGet]
        public IActionResult GetAll() => Ok(_brandService.GetAll());

        [HttpPost("Create")]
        public IActionResult Create([FromBody] BrandRequest request)
        {
            var id = _brandService.Create(request);
            return CreatedAtAction(nameof(Get), new { id });
        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest request)
        {
            var updated = _brandService.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }
        [HttpPatch("{id}/toggleStatus")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var status = _brandService.ToggleStatus(id);
            return status ? Ok() : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _brandService.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                _brandService.DeleteAll();
                return Ok(new { message = _localizer["Deleted"].Value });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = _localizer["Empty"].Value });
            }
        }
    }
}
