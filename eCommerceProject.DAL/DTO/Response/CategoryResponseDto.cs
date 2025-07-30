using eCommerceProject.Model.Category;

namespace eCommerceProject.DTO.Response
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public List<CategoryTranslationResponse> CategoryTranslations { get; set; } 
    }
}
