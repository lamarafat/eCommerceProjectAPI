using eCommerceProject.Model.Category;

namespace eCommerceProject.DTO.Response
{
    public class BrandResponseDto
    {
        public int Id { get; set; }
        public List<BrandTranslationResponse> BrandTranslations { get; set; } 
    }
}
