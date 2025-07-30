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
        private readonly ApplicationDbContext context;

        public CategoriesController(IStringLocalizer<SharedResource> localizer, ApplicationDbContext context)
        {
            _localizer = localizer;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cats = context.Categories.OrderByDescending(c => c.CreatedAt).ToList().Adapt<List<CategoryResponseDto>>();
            return Ok(new { message = _localizer["Success"].Value, cats });
        }
        [HttpGet("{id}")]
        public IActionResult Details([FromRoute]int id)
        {

            var category = context.Categories
    .Include(c => c.CategoryTranslations)
    .FirstOrDefault(c => c.Id == id);
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
            context.Categories.Add(category);
            context.SaveChanges();
            return Ok(new { message = _localizer["Created"].Value });
        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]CategoryRequestDto request)
        {
            var category = context.Categories.Include(c => c.CategoryTranslations).FirstOrDefault(c => c.Id == id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            //category.Status = request.Status; عملتها كومنت لانه انا مش حاطة في ال requestDto  اشي لل status  بس في حال حطيت او حطيت نوع تاني بكتبهم بهاي الطريقة طالما مش موجودين جوا translation ال  
            foreach (var translationRequest in request.CategoryTranslations)
            {
                var existingTranslation = category.CategoryTranslations.FirstOrDefault(t => t.Language == translationRequest.Language);
                if (existingTranslation is not null)
                {
                    existingTranslation.Name = translationRequest.Name;
                }
                else
                {
                    category.CategoryTranslations.Add(new CategoryTranslation
                    {
                        Name = translationRequest.Name,
                        Language = translationRequest.Language,
                        CategoryId = category.Id
                    });
                }
            }
            context.SaveChanges();
            return Ok(new { message = _localizer["Updated"].Value });
        }

        [HttpPatch("{id}/toggleStatus")]
        public IActionResult ToggleStatus([FromRoute]int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            category.Status = category.Status == Status.Active ? Status.In_active : Status.Active;
            context.SaveChanges();
            return Ok(new { message = _localizer["Success"].Value });
        }

        [HttpGet(" ")]
        public IActionResult GetAll([FromQuery]string lang = "en")
        {
            var categories = context.Categories
                .Include(c => c.CategoryTranslations)
                .Where(c => c.Status == Status.Active)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            var result = categories.Select(cat => new
            {
                Id = cat.Id,
                Name = cat.CategoryTranslations.FirstOrDefault(t => t.Language == lang)?.Name
            });

            return Ok(new { message = _localizer["Success"].Value, data = result });
        }
        [HttpGet("all")]
        public IActionResult GetAllCategories()
        {
            var cats = context.Categories
    .Include(c => c.CategoryTranslations)
    .OrderByDescending(c => c.CreatedAt)
    .ToList()
    .Adapt<List<CategoryResponseDto>>();
            return Ok(new { message = _localizer["Success"].Value, cats });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound(new { message = _localizer["NotFound"].Value });
            }
            context.Remove(category);
            context.SaveChanges();
            return Ok(new { message = _localizer["Deleted"].Value });
        }
        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll()
        {
            var categories = context.Categories.ToList();
            if (!categories.Any())
            {
                return NotFound(new { message = _localizer["Empty"].Value });
            }
            context.RemoveRange(categories);
            context.SaveChanges();
            return Ok(new { message = _localizer["Deleted"].Value });
        }
        

    }
}
